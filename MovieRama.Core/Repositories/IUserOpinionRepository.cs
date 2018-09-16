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
	public interface IUserOpinionRepository : IRepository
	{
		Task<UserOpinion> GetUserOpinionAsync(int user, int movie);
		Task<IEnumerable<UserOpinion>> GetUserOpinionsAsync(int user, IEnumerable<int> movies);
		void Add(UserOpinion opinion);
        void Update(UserOpinion opinion);
		void Remove(UserOpinion opinion);
	}
}
