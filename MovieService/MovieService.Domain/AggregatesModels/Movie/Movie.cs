using MovieService.Domain.Events;
using MovieService.Domain.Exceptions;
using MovieService.Domain.SeedWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MovieService.Domain.AggregatesModels.Movie
{
    public class Movie : Entity, IAggregateRoot
    {

        public double? Popularity { get;private set; }
        public string PosterPath { get;private set; }
        public bool? IsAdult { get;private set; }
        public string BackdropPath { get;private set; }
        public string OriginalLanguage { get;private set; }
        public string OriginalTitle { get; private set; }
        public string Title { get; private set; }
        public string Overview { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        
        private   Collection<Review> _reviews;
        [JsonProperty("Reviews")]
        public virtual IReadOnlyCollection<Review> Reviews
        {
            get { return _reviews.ToArray(); }
        }
        public double VoteAverage => GetAverageVote();


        private double GetAverageVote()
        {
            if (_reviews!=null &&  _reviews.Any(p => p.Rating > 0))
            {
              return  _reviews.Where(p => p.Rating > 0).Average(p => p.Rating);
            }

            return 0;
        }

        public Movie(int id, [NotNull]string title, [NotNull]DateTime releaseDate) 
        {
            if (id <= 0) throw new MovieServiceDomainException("Id can not be negative or zero");
            Id = id;
            Title = Check.NotNullOrWhiteSpace(value:title,nameof(title));
            ReleaseDate = Check.NotNull<DateTime>(value: releaseDate, nameof(releaseDate));
            _reviews = new Collection<Review>();

            AddDomainEvent(new MovieCreatedDomainEvent(this));
        }
        private Movie() {
            _reviews = new Collection<Review>();
        }
      

        public void SetPopularity(double popularity)
        {
            Popularity = popularity;
        }
        public void SetPosterPath(string posterPath)
        {
            PosterPath = posterPath;
        }
        public void SetIsAdult(bool isAdult)
        {
            IsAdult = isAdult;
        }
        public void SetBackDropPath(string backdropPath)
        {
            BackdropPath = backdropPath;
        }
        public void SetOrginalLanguage(string orginalLanguage)
        {
            OriginalLanguage = orginalLanguage;
        }

        public void SetOriginalTitle(string originalTitle)
        {
            OriginalTitle = originalTitle;
        }

        public void SetTitle([NotNull]string title)
        {
            Title = Check.NotNullOrWhiteSpace(value: title, nameof(title));
        }
        public void SetOverview(string overview)
        {
            Overview = overview;
        }
        public void SetReleaseDate([NotNull]DateTime releaseDate)
        {
            ReleaseDate = Check.NotNull<DateTime>(value: releaseDate, nameof(releaseDate));
        }

        public Review AddReview([NotNull]Review review)
        {
            _reviews = _reviews ?? new Collection<Review>();
            review = Check.NotNull<Review>(review, nameof(review));

            //not check for userId. We can add a lot of review to try for sample
            var isExist = _reviews.Any(p => p.Id == review.Id);
            if (!isExist)
            {
                _reviews.Add(review);
                AddDomainEvent(new MovieReviewsUpdatedDomainEvent(this));
                return review;
            }
            else
            {
                throw new MovieServiceDomainException("Review Exist!");
            }
        }

        public Review RemoveReview([NotNull]Review review)
        {
            review = Check.NotNull<Review>(review, nameof(review));

            var isExist = _reviews.Any(p => p.Id == review.Id);
            if (isExist)
            {
                _reviews.Remove(review);
                AddDomainEvent(new MovieReviewsUpdatedDomainEvent(this));
                return review;
            }
            else
            {
                throw new MovieServiceDomainException("Review not  Exist!");
            }
        }

        public Review UpdateReview([NotNull]Review review)
        {
            review = Check.NotNull<Review>(review, nameof(review));

            var updateTo = _reviews.FirstOrDefault(p => p.Id == review.Id);
            if (updateTo != null)
            {
                updateTo = review;
                AddDomainEvent(new MovieReviewsUpdatedDomainEvent(this));
                return review;
            }
            else
            {
                throw new MovieServiceDomainException("Review not  Exist!");
            }
        }









    }
}
