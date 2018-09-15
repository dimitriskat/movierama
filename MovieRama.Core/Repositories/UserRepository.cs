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
    public class UserRepository : RepositoryBase, IUserRepository
	{
        private readonly MovieRamaContext _context;

		public UserRepository(MovieRamaContext context)
		{
            _context = context;
		}

		public async Task<User> GetUserAsync(int id)
        {
			User user = await _context.User.FindAsync(id);

            return user;
        }

		public Task<User> GetUserByUserNameAsync(string userName)
		{
			return _context.User.SingleOrDefaultAsync(x => x.Username == userName);
		}

		public async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> ids)
		{
			IEnumerable<User> users = await _context.User.Where(x => ids.Contains(x.Id)).ToListAsync();

			return users;
		}

		public void Add(User user)
		{
			_context.User.Add(user);
		}

		public void Update(User user)
		{
			_context.User.Update(user);
		}

		public Task<bool> Exists(string userName)
		{
			return _context.User.AnyAsync(x => x.Username == userName);
		}
	}
}
