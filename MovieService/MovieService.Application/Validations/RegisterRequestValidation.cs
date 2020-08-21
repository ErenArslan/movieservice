using FluentValidation;
using MovieService.Application.Dtos.Requests;

namespace MovieService.Application.Validations
{
    public class RegisterRequestValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidation()
        {

            RuleFor(x => x.Fullname)
              .NotNull()
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(30)
              .WithMessage("Uygun Ad Soyad Giriniz");

            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15)
                .WithMessage("Uygun Kullanıcı Adı Giriniz");

            RuleFor(x => x.Email)
               .NotNull()
               .NotEmpty()
               .EmailAddress()
               .WithMessage("Uygun Email Giriniz");




            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15)
                .WithMessage("Uygun Şifre Giriniz.");
        }
    }



}
