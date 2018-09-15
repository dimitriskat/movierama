using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRama.Core.Models
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime LastUpdateTime { get; set; }
	}
}

