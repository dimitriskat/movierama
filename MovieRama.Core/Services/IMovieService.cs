using MovieRama.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Services
{
	/// <summary>
	/// Provides a set of operations such as getting and posting movies
	/// </summary>
    public interface IMovieService
    {
		Task<IEnumerable<MovieDto>> ListMoviesAsync(MovieCriteriaDto criteria, int? userId);
		Task<MovieDto> GetByIdAsync(int id);
		Task<int> PostAsync(int user, MoviePostDto moviePostDto);
    }
}
