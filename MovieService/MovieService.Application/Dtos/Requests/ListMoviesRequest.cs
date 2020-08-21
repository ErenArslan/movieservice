using MediatR;
using MovieService.Application.Dtos.Responses;

namespace MovieService.Application.Dtos.Requests
{
    public class ListMoviesRequest : IRequest<BaseResponse<MovieDto>>
    {
        public int Page { get; set; }
        public int Offset { get; set; }
    }
}
