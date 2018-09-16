using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Services
{
	/// <summary>
	/// Provides a set of operations such as expressing or revoking user opinions
	/// </summary>
	public interface IUserOpinionService
	{
        Task LikeAsync(int user, int movie);
		Task HateAsync(int user, int movie);
		Task RevokeAsync(int userId, int movieId);
	}
}
