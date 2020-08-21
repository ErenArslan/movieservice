using MediatR;
using MovieService.Application.Dtos.Responses;

namespace MovieService.Application.Dtos.Requests
{
    public  class RegisterRequest:IRequest<BaseResponse<bool>>
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
