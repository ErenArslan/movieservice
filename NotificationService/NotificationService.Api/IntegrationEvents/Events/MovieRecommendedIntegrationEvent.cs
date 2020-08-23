using System;

namespace NotificationService.Api.IntegrationEvents.Events
{
    public  class MovieRecommendedIntegrationEvent
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Overview { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public string TargetEmail { get; private set; }

        public MovieRecommendedIntegrationEvent(int id, string title, string overview, DateTime releaseDate, string targetEmail)
        {
            Id = id;
            Title = title;
            Overview = overview;
            ReleaseDate = releaseDate;
            TargetEmail = targetEmail;
        }
    }
}
