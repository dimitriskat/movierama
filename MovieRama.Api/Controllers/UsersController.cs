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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody]UserDto userDto)
        {
            var user = await _authenticationService.AuthenticateAsync(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

			var token = _authenticationService.GenerateAccessToken(user.Id);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                user.Id,
                user.Username,
                user.FirstName,
                user.LastName,
                Token = token
            });
        }

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> RegisterAsync([FromBody]UserDto userDto)
		{
			int id = await _userService.CreateAsync(userDto, userDto.Password);
			return CreatedAtAction("GetAsync", new { id }, id);
		}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
			if (user == null) return NotFound();
			return Ok(user);
        }
    }
}
