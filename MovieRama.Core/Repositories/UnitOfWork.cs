using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRama.Core.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private MovieRamaContext _context;

		public UnitOfWork(MovieRamaContext context)
		{
			_context = context;
		}

		public void BeginTransaction()
		{
			_context.Database.BeginTransaction(System.Data.IsolationLevel.Snapshot);
		}

		public void CommitTransaction()
		{
			_context.Database.CommitTransaction();
		}

		public void RollbackTransaction()
		{
			_context.Database.RollbackTransaction();
		}

		public Task SaveChangesAsync()
		{
			return _context.SaveChangesAsync();
		}
    }
}
