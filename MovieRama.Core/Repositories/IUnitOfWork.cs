using MovieRama.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Repositories
{
	public interface IUnitOfWork
	{
		void BeginTransaction();
		void CommitTransaction();
		void RollbackTransaction();

		Task SaveChangesAsync();
	}
}
