using MediatR;
using MovieService.Domain.AggregatesModels.Movie;
using System.Diagnostics.CodeAnalysis;

namespace MovieService.Domain.Events
{
    public class MovieReviewsUpdatedDomainEvent : INotification
    {
        public Movie Movie { get; private set; }

        public MovieReviewsUpdatedDomainEvent([NotNull]Movie movie)
        {
            Movie = movie;
        }
    }
}
