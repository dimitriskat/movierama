using MovieRama.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Services
{
    public interface IUserService
    {
		Task<int> CreateAsync(UserDto user, string password);
		Task<UserDto> AuthenticateAsync(string username, string password);
		Task<UserDto> GetByIdAsync(int id);
	}
}
