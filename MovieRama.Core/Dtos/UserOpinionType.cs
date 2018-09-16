using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRama.Core.Dtos
{
	/// <summary>
	/// Defines user opinions. Undefined if user hasn't posted any.
	/// </summary>
	public enum UserOpinionType
	{
		Like = 0,
		Hate = 1,
		Undefined = 2
	}
}
