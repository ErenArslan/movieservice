using MediatR;
using MovieService.Domain.AggregatesModels.Movie;
using System.Diagnostics.CodeAnalysis;

namespace MovieService.Domain.Events
{
   public class MovieCreatedDomainEvent:INotification
    {
        public Movie Movie { get;private set; }

        public MovieCreatedDomainEvent([NotNull]Movie movie)
        {
            Movie = movie;
        }
    }
}
