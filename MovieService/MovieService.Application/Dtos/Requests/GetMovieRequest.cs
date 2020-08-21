using MediatR;
using MovieService.Application.Dtos.Responses;

namespace MovieService.Application.Dtos.Requests
{
   public class GetMovieRequest:IRequest<BaseResponse<MovieDto>>
    {
        public int Id { get; set; }
    }
}
