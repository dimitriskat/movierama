using System;
using System.Collections.Generic;

namespace MovieRama.Core.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int User { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int Likes { get; set; }
        public int Hates { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
