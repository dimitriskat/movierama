using MovieRama.Core.Criteria;
using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Repositories
{
    public interface IMovieRepository
    {
		Task<IEnumerable<Movie>> ListMoviesAsync(MovieCriteria criteria);
		Task<Movie> GetMovieAsync(int id);
		void Add(Movie movie);
		void Update(Movie movie);
	}
}
