using MovieRama.Core.Criteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRama.Core.Dtos
{
	public class CriteriaBaseDto
	{
		public string SortField { get; set; }
		public SortOrder? SortOrder { get; set; }
		public int? Offset { get; set; }
		public int? Count { get; set; }
    }
}
