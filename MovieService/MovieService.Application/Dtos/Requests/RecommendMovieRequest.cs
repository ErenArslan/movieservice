using MediatR;
using MovieService.Application.Dtos.Responses;

namespace MovieService.Application.Dtos.Requests
{
    public class RecommendMovieRequest : IRequest<BaseResponse<bool>>
    {
        public string Email { get; set; }
    }
}
