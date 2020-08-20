using MovieService.Domain.Exceptions;
using MovieService.Domain.SeedWork;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MovieService.Domain.AggregatesModels.Movie
{
    public class Review:Entity<Guid>
    {
        public int Rating { get;private set; }
        public string Note { get; private set; }
        public Guid UserId { get; private set; }

        public Review([NotNull]Guid id, [NotNull]Guid userId, [NotNull]string note, int rating )
        {
          
            Id = Check.NotNull(value: id, nameof(id)); 
            UserId = Check.NotNull(value: userId, nameof(userId)); 
            Note = Check.NotNullOrWhiteSpace(value: note, nameof(note));

            if (rating > 0 && rating <= 10)
            {
                Rating = rating;
            }
            else
            {
                throw new MovieServiceDomainException("Rating is not in valid range");
            }

        }


        public void SetRating(int rating)
        {
            if (rating > 0 && rating <= 10)
            {
                Rating = rating;
            }
            else
            {
                throw new MovieServiceDomainException("Rating is not in valid range");
            }

        }

        public void SetNote([NotNull]string note)
        {
            Note = Check.NotNullOrWhiteSpace(value: note, nameof(note));
        }

        public void SetUser([NotNull]Guid userId)
        {
            UserId = Check.NotNull(value: userId, nameof(userId));
        }
    }
}
