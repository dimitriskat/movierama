using MovieRama.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MovieRama.Api.Authentication
{
	public interface IAuthenticationService
	{
		/// <summary>
		/// Checks if user has valid credentials and returns the user object
		/// </summary>
		Task<UserDto> AuthenticateAsync(string username, string password);

		/// <summary>
		/// Generates an access token for the provided user
		/// </summary>
		string GenerateAccessToken(int userId);

		/// <summary>
		/// Returns the user id for the provided identity object
		/// </summary>
		int GetUserId(IIdentity identity);
	}
}
