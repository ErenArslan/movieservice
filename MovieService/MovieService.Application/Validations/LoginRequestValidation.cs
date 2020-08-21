using FluentValidation;
using MovieService.Application.Dtos.Requests;

namespace MovieService.Application.Validations
{
    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.Identifier).NotNull().NotEmpty().WithMessage("Email/Kullanıcı Adı Giriniz");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Şifre Giriniz.");
        }
    }



}
