using MediatR;
using MovieService.Application.Dtos.Responses;

namespace MovieService.Application.Dtos.Requests
{
    public class LoginRequest : IRequest<BaseResponse<LoginResponseDto>>
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
}
