using MovieRama.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Repositories
{
	/// <summary>
	/// Coordinates the execution of a business transaction
	/// </summary>
	public interface IUnitOfWork
	{
		void BeginTransaction();
		void CommitTransaction();
		void RollbackTransaction();

		Task SaveChangesAsync();
	}
}
