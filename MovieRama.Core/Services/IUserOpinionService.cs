using MovieRama.Core.Dtos;
using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Services
{
    public interface IUserOpinionService
	{
        Task LikeAsync(int user, int movie);
		Task HateAsync(int user, int movie);
		Task RevokeAsync(int userId, int movieId);
	}
}
