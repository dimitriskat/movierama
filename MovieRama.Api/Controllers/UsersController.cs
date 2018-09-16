using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRama.Api.Authentication;
using MovieRama.Core.Dtos;
using MovieRama.Core.Services;

namespace MovieRama.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
		private readonly IAuthenticationService _authenticationService;

        public UsersController(
            IUserService userService,
			IAuthenticationService authenticationService)
        {
            _userService = userService;
			_authenticationService = authenticationService;
        }

		// POST api/user/authenticate
		[AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody]CredentialsDto credentials)
        {
            var user = await _authenticationService.AuthenticateAsync(credentials.Username, credentials.Password);

            if (user == null) return BadRequest("Username or password is incorrect");

			var token = _authenticationService.GenerateAccessToken(user.Id);

            // return basic user info (without password) and token to store client side
            return Ok(new AccessTokenDto()
			{
                Id = user.Id,
				Username = user.Username,
				FirstName = user.FirstName,
				LastName = user.LastName,
                Token = token
            });
        }

		// POST api/user/register
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> RegisterAsync([FromBody]UserDto userDto)
		{
			int id = await _userService.CreateAsync(userDto, userDto.Password);
			return CreatedAtAction("GetAsync", new { id }, id);
		}

		// GET api/user/{id}
		[HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
			if (user == null) return NotFound();
			return Ok(user);
        }
    }
}
