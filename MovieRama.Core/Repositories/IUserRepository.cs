using MovieRama.Core.Criteria;
using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Repositories
{
	/// <summary>
	/// Used by the service layer in order access domain objects
	/// </summary>
	public interface IUserRepository : IRepository
	{
		Task<User> GetUserAsync(int id);
		Task<User> GetUserByUserNameAsync(string userName);
		Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> ids);
		Task<bool> Exists(string userName);
		void Add(User user);
		void Update(User user);
	}
}
