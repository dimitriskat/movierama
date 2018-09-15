using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieRama.Core.Criteria
{
	public class MovieQueryBuilder: QueryBuilderBase
	{
		public IQueryable<Movie> BuildQueryable(MovieCriteria criteria, IQueryable<Movie> initial)
		{
			IQueryable<Movie> queryable = initial;

			if (criteria.User.HasValue) queryable = queryable.Where(x => x.User == criteria.User.Value);

			if (ShouldSort(criteria)) queryable = ApplySorting(queryable, criteria);

			queryable = ApplyPaging(queryable, criteria);

			return queryable;
		}

		private IQueryable<Movie> ApplySorting(IQueryable<Movie> queryable, MovieCriteria criteria)
		{
			switch (criteria.SortField)
			{
				case "likes": return ApplySortDirection(queryable, x => x.Likes, criteria);
				case "hates": return ApplySortDirection(queryable, x => x.Hates, criteria);
				case "date": return ApplySortDirection(queryable, x => x.CreationTime, criteria);
				default: throw new Exception("Invalid sort field specified");
			}
		}
	}
}
