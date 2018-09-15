using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRama.Api.Authentication;
using MovieRama.Api.Controllers;
using MovieRama.Core.Dtos;
using MovieRama.Core.Services;
using System;
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
		public async Task GetAsync_WhenCalled_ReturnsOkResultAsync()
		{
			// Setup
			MockedMovieService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new MovieDto());

			// Operation
			var okResult = await Controller.GetAsync(1);

			// Assertion
			Assert.IsType<OkObjectResult>(okResult);
		}

		[Fact]
		public async Task GetAsync_WhenItemNotExists_ReturnsNotFound()
		{
			// Setup
			MockedMovieService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((MovieDto)null);

			// Operation
			var notFoundResult = await Controller.GetAsync(1);
			
			// Assertion
			Assert.IsType<NotFoundResult>(notFoundResult);
		}
	}
}
