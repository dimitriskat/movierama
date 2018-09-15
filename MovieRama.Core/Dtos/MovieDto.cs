using MovieRama.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRama.Core.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int User { get; set; }
		public string UserFirstName { get; set; }
		public string UserLastName { get; set; }
		public DateTime? PublicationDate { get; set; }
        public int Likes { get; set; }
        public int Hates { get; set; }
		public UserOpinionType UserOpinion { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
