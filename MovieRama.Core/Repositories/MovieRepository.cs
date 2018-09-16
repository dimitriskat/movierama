using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieRama.Core.Criteria;

namespace MovieRama.Core.Repositories
{
    public class MovieRepository : IMovieRepository
	{
        private readonly MovieRamaContext _context;

		public MovieRepository(MovieRamaContext context)
		{
            _context = context;
		}

		public async Task<IEnumerable<Movie>> ListMoviesAsync(MovieCriteria criteria)
		{
			IQueryable<Movie> queryable = new MovieQueryBuilder().BuildQueryable(criteria, _context.Movie);

			IEnumerable<Movie> movies = await queryable.ToListAsync();

			return movies;
		}

		public async Task<Movie> GetMovieAsync(int id)
        {
            Movie movie = await _context.Movie.FindAsync(id);

            return movie;
        }

		public void Add(Movie movie)
		{
			_context.Movie.Add(movie);
		}

		public void Update(Movie movie)
		{
			_context.Movie.Update(movie);
		}
	}
}
