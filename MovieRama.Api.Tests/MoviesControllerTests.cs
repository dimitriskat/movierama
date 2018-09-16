using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRama.Api.Authentication;
using MovieRama.Api.Controllers;
using MovieRama.Core.Dtos;
using MovieRama.Core.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MovieRama.Api.Tests
{
	public class MoviesControllerTests
	{
		public MoviesController Controller { get; set; }
		public Mock<IMovieService> MockedMovieService { get; set; }
		public Mock<IUserOpinionService> MockedUserOpinionService { get; set; }
		public Mock<IAuthenticationService> MockedAuthenticationService { get; set; }

		public MoviesControllerTests()
		{
			MockedMovieService = new Mock<IMovieService>();
			MockedUserOpinionService = new Mock<IUserOpinionService>();
			MockedAuthenticationService = new Mock<IAuthenticationService>();
			Controller = new MoviesController(MockedMovieService.Object,
				MockedUserOpinionService.Object,
				MockedAuthenticationService.Object);
		}

		[Fact]
		public async Task ListAsync_WhenCalled_ReturnsOkResult()
		{
			// Setup
			SetUpTestUser();
			var testData = GetMovieDtoTestData();
			MockedMovieService.Setup(x => x.ListMoviesAsync(It.IsAny<MovieCriteriaDto>(), It.IsAny<int?>())).ReturnsAsync(testData);

			// Operation
			var actionResult = await Controller.ListAsync(new MovieCriteriaDto());

			// Assertion
			var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);//Is ok object result

			Assert.NotNull(okObjectResult.StatusCode);//Has status code defined
			Assert.Equal((int)System.Net.HttpStatusCode.OK, okObjectResult.StatusCode.Value);//200 status code

			var response = Assert.IsAssignableFrom<IEnumerable<MovieDto>>(okObjectResult.Value);//Response contains movies
			Assert.Equal(2, response.Count());//Test data count
		}

		[Fact]
		public async Task GetAsync_WhenCalled_ReturnsOkResult()
		{
			// Setup
			var testData = GetMovieDtoTestData();
			MockedMovieService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testData.First());

			// Operation
			var actionResult = await Controller.GetAsync(1);

			// Assertion
			var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);//Is ok object result

			Assert.NotNull(okObjectResult.StatusCode);//Has status code defined
			Assert.Equal((int)System.Net.HttpStatusCode.OK, okObjectResult.StatusCode.Value);//200 status code

			Assert.IsAssignableFrom<MovieDto>(okObjectResult.Value);//Response contains movie
		}

		[Fact]
		public async Task GetAsync_WhenItemNotExists_ReturnsNotFound()
		{
			// Setup
			MockedMovieService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((MovieDto)null);

			// Operation
			var actionResult = await Controller.GetAsync(1);
			
			// Assertion
			var notFoundResult = Assert.IsType<NotFoundResult>(actionResult);//Is not found action result

			Assert.Equal((int)System.Net.HttpStatusCode.NotFound, notFoundResult.StatusCode);//404 status code
		}

		[Fact]
		public async Task PostAsync_WhenItemValid_ReturnsCreated()
		{
			// Setup
			SetUpTestUser();
			MockedMovieService.Setup(x => x.PostAsync(It.IsAny<int>(), It.IsAny<MoviePostDto>())).ReturnsAsync(1);

			// Operation
			var actionResult = await Controller.Post(GetMoviePostTestData());

			// Assertion
			CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);//Is created action result

			Assert.NotNull(createdAtActionResult.StatusCode);//Has status code defined
			Assert.Equal((int)System.Net.HttpStatusCode.Created, createdAtActionResult.StatusCode.Value);//201 status code
		}

		[Fact]
		public async Task LikeAsync_WhenCalled_ReturnsOkResult()
		{
			// Setup
			SetUpTestUser();

			// Operation
			var actionResult = await Controller.LikeAsync(1);

			// Assertion
			var okResult = Assert.IsType<OkResult>(actionResult);//Is ok object result

			Assert.Equal((int)System.Net.HttpStatusCode.OK, okResult.StatusCode);//200 status code
		}

		[Fact]
		public async Task HateAsync_WhenCalled_ReturnsOkResult()
		{
			// Setup
			SetUpTestUser();

			// Operation
			var actionResult = await Controller.HateAsync(1);

			// Assertion
			var okResult = Assert.IsType<OkResult>(actionResult);//Is ok object result

			Assert.Equal((int)System.Net.HttpStatusCode.OK, okResult.StatusCode);//200 status code
		}

		[Fact]
		public async Task RevokeAsync_WhenCalled_ReturnsOkResult()
		{
			// Setup
			SetUpTestUser();

			// Operation
			var actionResult = await Controller.RevokeAsync(1);

			// Assertion
			var okResult = Assert.IsType<OkResult>(actionResult);//Is ok object result

			Assert.Equal((int)System.Net.HttpStatusCode.OK, okResult.StatusCode);//200 status code
		}

		private void SetUpTestUser()
		{
			Controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
						new Claim(ClaimTypes.Name, "1")
					}))
				}
			};
		}

		private IEnumerable<MovieDto> GetMovieDtoTestData()
		{
			var dtos = new List<MovieDto>()
			{
				new MovieDto()
				{
					Id = 1,
					Title = "Test movie 1",
					Description = "Test movie 1 description"
				},
				new MovieDto()
				{
					Id = 2,
					Title = "Test movie 2",
					Description = "Test movie 2 description"
				}
			};
			return dtos;
		}

		private MoviePostDto GetMoviePostTestData()
		{
			return new MoviePostDto()
			{
				Title = "Test movie 1",
				Description = "Test movie 1 description"
			};
		}
	}
}
