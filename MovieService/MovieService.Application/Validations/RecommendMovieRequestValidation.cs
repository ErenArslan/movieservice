using FluentValidation;
using MovieService.Application.Dtos.Requests;

namespace MovieService.Application.Validations
{
    public  class RecommendMovieRequestValidation : AbstractValidator<RecommendMovieRequest>
    {
        public RecommendMovieRequestValidation()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.MovieId).GreaterThan(0);
        }
    }



}
