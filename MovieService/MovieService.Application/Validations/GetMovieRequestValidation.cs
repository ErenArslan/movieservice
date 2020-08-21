using FluentValidation;
using MovieService.Application.Dtos.Requests;

namespace MovieService.Application.Validations
{
    public class GetMovieRequestValidation : AbstractValidator<GetMovieRequest>
    {
        public GetMovieRequestValidation()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }



}
