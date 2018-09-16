using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MovieRama.Core.Criteria
{
	/// <summary>
	/// Contains fields that are used by all concrete criteria implementations such as sorting and paging
	/// </summary>
	public abstract class CriteriaBase
	{
		public string SortField { get; set; }
		public SortOrder? SortOrder { get; set; }
		public int? Offset { get; set; }
		public int? Count { get; set; }
	}
}
