using System;
using System.Collections.Generic;

namespace MovieRama.Core.Models
{
	public class UserOpinion
	{
		public int User { get; set; }
		public int Movie { get; set; }
		public OpinionType Opinion { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime LastUpdateTime { get; set; }
	}
}
