using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRama.Api.Authentication
{
	public class AccessTokenDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Token { get; set; }
	}
}
