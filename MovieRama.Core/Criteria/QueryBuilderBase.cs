using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MovieRama.Core.Criteria
{
	public class QueryBuilderBase
	{
		protected bool ShouldSort(CriteriaBase criteria)
		{
			return !string.IsNullOrWhiteSpace(criteria.SortField) && 
				criteria.SortOrder.HasValue;
		}

		public IOrderedQueryable<TSource> ApplySortDirection<TSource, TKey>(IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, CriteriaBase criteria)
		{
			switch (criteria.SortOrder.Value)
			{
				case SortOrder.desc:
					return (source.OrderByDescending(keySelector));
				default:
					return (source.OrderBy(keySelector));
			}
		}

		protected IQueryable<TSource> ApplyPaging<TSource>(IQueryable<TSource> source, CriteriaBase criteria)
		{
			return (criteria.Offset.HasValue && criteria.Offset.Value > -1 && criteria.Count.HasValue && criteria.Count.Value > 0) ?
				source.Skip(criteria.Offset.Value).Take(criteria.Count.Value) : source;
		}
	}
}
