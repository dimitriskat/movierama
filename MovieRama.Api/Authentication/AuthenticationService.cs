using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using MovieRama.Api.Settings;
using MovieRama.Core.Dtos;
using MovieRama.Core.Services;

namespace MovieRama.Api.Authentication
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IUserService _userService;
		private readonly AppSettings _appSettings;

		public AuthenticationService(
			IUserService userService,
			IOptions<AppSettings> appSettings)
		{
			_userService = userService;
			_appSettings = appSettings.Value;
		}

		public Task<UserDto> AuthenticateAsync(string username, string password)
		{
			return _userService.AuthenticateAsync(username, password);
		}

		public string GenerateAccessToken(int userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, userId.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);
			return tokenString;
		}

		public int GetUserId(IIdentity identity)
		{
			var claimsIdentity = (ClaimsIdentity)identity;
			Claim userClaim = claimsIdentity.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Name);
			if (userClaim == null) throw new Exception("Invalid user provided");
			int user;
			if (!int.TryParse(userClaim.Value, out user)) throw new Exception("Invalid user provided");
			return user;
		}
	}
}
