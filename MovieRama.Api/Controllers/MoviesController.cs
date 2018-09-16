using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRama.Api.Authentication;
using MovieRama.Core.Dtos;
using MovieRama.Core.Services;

namespace MovieRama.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class MoviesController : ControllerBase
	{
		private readonly IMovieService _movieService;
		private readonly IUserOpinionService _userOpinionService;
		private readonly IAuthenticationService _authenticationService;

		public MoviesController(IMovieService movieService,
			IUserOpinionService userOpinionService,
			IAuthenticationService authenticationService)
		{
			_movieService = movieService;
			_userOpinionService = userOpinionService;
			_authenticationService = authenticationService;
		}

		// GET api/movies
		[AllowAnonymous]
		[HttpGet("")]
		public async Task<ActionResult<IEnumerable<MovieDto>>> ListAsync([FromQuery] MovieCriteriaDto criteria)
		{
			//Movies are listed without user information for anonymous user
			int? userId = User.Identity.IsAuthenticated ?
				(int?)_authenticationService.GetUserId(User.Identity) :
				null;

			var dtos = await _movieService.ListMoviesAsync(criteria, userId);

			return Ok(dtos);
		}

		// GET api/movies/{id}
		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(int id)
		{
			var movie = await _movieService.GetByIdAsync(id);
			if (movie == null) return NotFound();
			return Ok(movie);
		}

		// POST api/movies
		[HttpPost]
		[ProducesResponseType(201)]
		public async Task<ActionResult<int>> Post([FromBody] MoviePostDto movie)
		{
			int user = _authenticationService.GetUserId(User.Identity);
			int id = await _movieService.PostAsync(user, movie);
			return CreatedAtAction("GetAsync", new { id }, id);
		}

		// PUT api/movies/{id}/like
		[HttpPut("{id}/like")]
		public async Task<ActionResult> LikeAsync(int id)
		{
			int user = _authenticationService.GetUserId(User.Identity);
			await _userOpinionService.LikeAsync(user, id);
			return Ok();
		}

		// PUT api/movies/{id}/hate
		[HttpPut("{id}/hate")]
		public async Task<ActionResult> HateAsync(int id)
		{
			int user = _authenticationService.GetUserId(User.Identity);
			await _userOpinionService.HateAsync(user, id);
			return Ok();
		}

		// PUT api/movies/{id}/opinionrevoke
		[HttpPut("{id}/opinionrevoke")]
		public async Task<ActionResult> RevokeAsync(int id)
		{
			int user = _authenticationService.GetUserId(User.Identity);
			await _userOpinionService.RevokeAsync(user, id);
			return Ok();
		}
	}
}
