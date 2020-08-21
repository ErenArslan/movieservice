using MediatR;
using MovieService.Application.Dtos.Responses;

namespace MovieService.Application.Dtos.Requests
{
    public class AddReviewRequest : IRequest<BaseResponse<bool>>
    {
        public int Rating { get; set; }
        public string Note { get; set; }
    }
}
