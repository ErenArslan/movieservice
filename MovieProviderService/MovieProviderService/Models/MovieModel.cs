using System;
using System.Collections.Generic;
using System.Text;

namespace MovieProviderService.Models
{
   public class MovieModel
    {
        public int Id { get; set; }
        public double popularity { get;  set; }
        public string poster_path { get;  set; }
        public bool adult { get;  set; }
        public string backdrop_path { get;  set; }
        public string original_language { get;  set; }
        public string original_title { get;  set; }
        public string title { get;  set; }
        public string overview { get;  set; }
        public DateTime releaseDate { get;  set; }
    }
}
