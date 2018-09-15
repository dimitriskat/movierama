using MovieRama.Core.Criteria;
using MovieRama.Core.Dtos;
using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Services
{
    public interface IMovieService
    {
		Task<IEnumerable<MovieDto>> ListMoviesAsync(MovieCriteriaDto criteria, int? userId);
		Task<MovieDto> GetByIdAsync(int id);
		Task<int> PostAsync(int user, MoviePostDto moviePostDto);
    }
}
