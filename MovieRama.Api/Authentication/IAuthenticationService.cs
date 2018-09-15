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
		Task<UserDto> AuthenticateAsync(string username, string password);
		string GenerateAccessToken(int userId);
		int GetUserId(IIdentity identity);
	}
}
