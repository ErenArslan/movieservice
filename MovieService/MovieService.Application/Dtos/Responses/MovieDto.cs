using System;
using System.Collections.ObjectModel;

namespace MovieService.Application.Dtos.Responses
{
    public  class MovieDto
    {
        public double Popularity { get;  set; }
        public string PosterPath { get;  set; }
        public bool IsAdult { get;  set; }
        public string BackdropPath { get;  set; }
        public string OriginalLanguage { get;  set; }
        public string OriginalTitle { get;  set; }
        public string Title { get;  set; }
        public string Overview { get;  set; }
        public DateTime ReleaseDate { get;  set; }

        public Collection<ReviewDto> Reviews;
    }
}
