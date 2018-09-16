using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieRama.Core.Criteria
{
	/// <summary>
	/// Allows filtering of movies based on predefined filters such as user id
	/// </summary>
	public class MovieCriteria : CriteriaBase
	{
		public int? User { get; set; }
	}
}
