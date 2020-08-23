using System;

namespace MovieProviderService.Events
{
   public class MovieFetchedIntegrationEvent
    {
        public int Id { get; set; }
        public double Popularity { get; private set; }
        public string PosterPath { get; private set; }
        public bool IsAdult { get; private set; }
        public string BackdropPath { get; private set; }
        public string OriginalLanguage { get; private set; }
        public string OriginalTitle { get; private set; }
        public string Title { get; private set; }
        public string Overview { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        public MovieFetchedIntegrationEvent(int id, double popularity, string posterPath, bool isAdult, string backdropPath,
           string originalLanguage, string originalTitle, string title, string overview, DateTime releaseDate)
        {
            Id = id;
            Popularity = popularity;
            PosterPath = posterPath;
            IsAdult = isAdult;
            BackdropPath = backdropPath;
            OriginalLanguage = originalLanguage;
            OriginalTitle = originalTitle;
            Title = title;
            Overview = overview;
            ReleaseDate = releaseDate;
        }
    }
}
