using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieRama.Core.Models;
using Microsoft.EntityFrameworkCore;
using MovieRama.Core.Repositories;
using AutoMapper;
using MovieRama.Api.Settings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieRama.Core.Services;
using FluentValidation.AspNetCore;
using MovieRama.Core.Validators;
using MovieRama.Api.Errors;

namespace MovieRama.Api
{
    public class Startup
    {
		private readonly ILogger<Startup> _logger;

		private IConfiguration Configuration { get; }

		public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
			_logger = logger;
			Configuration = configuration;
        }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.
				//Using .net core mvc framework
				AddMvc().
				//Adding fluent validation for dto/model validation
				AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MoviePostDtoValidator>());

			//Using entity framework as orm with sql server database
			services.AddDbContext<MovieRamaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			//Automapper for mapping domain objects to dtos and vice versa
			services.AddAutoMapper();

			// configure strongly typed settings objects
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			//Configure jwt authentication. Simple in memory token management
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			//Registering repositories
			services.AddScoped<IApplicationContext, ApplicationContext>();
			services.AddScoped<IMovieRepository, MovieRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserOpinionRepository, UserOpinionRepository>();

			//Registering services
			services.AddScoped<Authentication.IAuthenticationService, Authentication.AuthenticationService>();
			services.AddScoped<IMovieService, MovieService>();
			services.AddScoped<IUserOpinionService, UserOpinionService>();
			services.AddScoped<IUserService, UserService>();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Global cors policy - allowing everything
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

			//Global exception handling 
			app.ConfigureExceptionHandler(_logger);

			//app.UseHttpsRedirection();
			app.UseMvc();
        }
    }
}
