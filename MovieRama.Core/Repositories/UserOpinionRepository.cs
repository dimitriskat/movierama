using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieRama.Core.Repositories
{
    public class UserOpinionRepository : IUserOpinionRepository
	{
        private readonly MovieRamaContext _context;

        public UserOpinionRepository(MovieRamaContext context)
		{
            _context = context;
        }

        public void Add(UserOpinion opinion)
        {
            _context.UserOpinion.Add(opinion);
        }

        public Task<UserOpinion> GetUserOpinionAsync(int user, int movie)
		{
			return _context.UserOpinion.FindAsync(user, movie);
		}

		public async Task<IEnumerable<UserOpinion>> GetUserOpinionsAsync(int user, IEnumerable<int> movies)
		{
			IEnumerable<UserOpinion> opinions = await _context.UserOpinion.Where(x => x.User == user && movies.Contains(x.Movie)).ToListAsync();

			return opinions;
		}

		public void Update(UserOpinion opinion)
        {
            _context.UserOpinion.Update(opinion);
        }

        public void Remove(UserOpinion opinion)
        {
            _context.UserOpinion.Remove(opinion);
        }
	}
}
