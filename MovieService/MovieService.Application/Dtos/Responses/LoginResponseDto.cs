namespace MovieService.Application.Dtos.Responses
{
    public  class LoginResponseDto 
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
