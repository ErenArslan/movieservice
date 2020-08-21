using MediatR;
using MovieService.Application.Dtos.Responses;
using System.Collections.Generic;

namespace MovieService.Application.Dtos.Requests
{
    public class ListMoviesRequest : IRequest<BaseResponse<List<MovieDto>>>
    {
        public int Page { get; set; }
        public int Offset { get; set; }
    }
}
