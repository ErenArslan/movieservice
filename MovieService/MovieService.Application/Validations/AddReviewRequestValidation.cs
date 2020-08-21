using FluentValidation;
using MovieService.Application.Dtos.Requests;

namespace MovieService.Application.Validations
{
    public class AddReviewRequestValidation : AbstractValidator<AddReviewRequest>
    {
        public AddReviewRequestValidation()
        {
            RuleFor(x => x.Note).NotNull().NotEmpty();
            RuleFor(x => x.Rating).GreaterThan(0).LessThanOrEqualTo(10);
            RuleFor(x => x.MovieId).GreaterThan(0);
        }
    }



}
