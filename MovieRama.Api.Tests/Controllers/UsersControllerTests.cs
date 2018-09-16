using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRama.Api.Authentication;
using MovieRama.Api.Controllers;
using MovieRama.Core.Dtos;
using MovieRama.Core.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MovieRama.Api.Tests.Controllers
{
	public class UsersControllerTests
	{
		public UsersController Controller { get; set; }
		public Mock<IUserService> MockedUserService { get; set; }
		public Mock<IAuthenticationService> MockedAuthenticationService { get; set; }

		public UsersControllerTests()
		{
			MockedUserService = new Mock<IUserService>();
			MockedAuthenticationService = new Mock<IAuthenticationService>();
			Controller = new UsersController(MockedUserService.Object,
				MockedAuthenticationService.Object);
		}

		[Fact]
		public async Task AuthenticateAsync_WhenCalledForExistingUser_ReturnsToken()
		{
			// Setup
			SetUpTestUser();
			MockedAuthenticationService.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(GetUserTestData());
			MockedAuthenticationService.Setup(x => x.GenerateAccessToken(It.IsAny<int>())).Returns("valid token");

			// Operation
			var actionResult = await Controller.AuthenticateAsync(new CredentialsDto() { Username = "username", Password = "password" });

			// Assertion
			var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);//Is ok object result

			Assert.NotNull(okObjectResult.StatusCode);//Has status code defined
			Assert.Equal((int)System.Net.HttpStatusCode.OK, okObjectResult.StatusCode.Value);//200 status code

			Assert.IsAssignableFrom<AccessTokenDto>(okObjectResult.Value);//Response contains movie
		}

		[Fact]
		public async Task AuthenticateAsync_WhenCalledForNonExistingUser_ReturnsBadRequest()
		{
			// Setup
			SetUpTestUser();

			// Operation
			var actionResult = await Controller.AuthenticateAsync(new CredentialsDto() { Username = "username", Password = "password" });

			// Assertion
			var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);//Is bad request object result

			Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, badRequestObjectResult.StatusCode.Value);//500 status code
		}

		[Fact]
		public async Task GetAsync_WhenCalled_ReturnsOkResult()
		{
			// Setup
			var testData = GetUserTestData();
			MockedUserService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testData);

			// Operation
			var actionResult = await Controller.GetAsync(1);

			// Assertion
			var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);//Is ok object result

			Assert.NotNull(okObjectResult.StatusCode);//Has status code defined
			Assert.Equal((int)System.Net.HttpStatusCode.OK, okObjectResult.StatusCode.Value);//200 status code

			Assert.IsAssignableFrom<UserDto>(okObjectResult.Value);//Response contains user
		}

		[Fact]
		public async Task GetAsync_WhenItemNotExists_ReturnsNotFound()
		{
			// Setup
			MockedUserService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((UserDto)null);

			// Operation
			var actionResult = await Controller.GetAsync(1);

			// Assertion
			var notFoundResult = Assert.IsType<NotFoundResult>(actionResult);//Is not found action result

			Assert.Equal((int)System.Net.HttpStatusCode.NotFound, notFoundResult.StatusCode);//404 status code
		}

		[Fact]
		public async Task RegisterAsync_WhenItemValid_ReturnsCreated()
		{
			// Setup
			SetUpTestUser();
			MockedUserService.Setup(x => x.CreateAsync(It.IsAny<UserDto>(), It.IsAny<string>())).ReturnsAsync(1);

			// Operation
			var actionResult = await Controller.RegisterAsync(GetUserTestData());

			// Assertion
			CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult);//Is created action result

			Assert.NotNull(createdAtActionResult.StatusCode);//Has status code defined
			Assert.Equal((int)System.Net.HttpStatusCode.Created, createdAtActionResult.StatusCode.Value);//201 status code
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

		private UserDto GetUserTestData()
		{
			return new UserDto()
			{
				Id = 1,
				FirstName = "First name",
				LastName = "Last name",
				Username = "User name"
			};
		}
	}
}
