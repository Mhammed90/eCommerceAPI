using eCommerce.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerce.Application.Validation.Authentication;

public class LoginUserValidator : AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress()
            .WithMessage("Invalid email format.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
    }
}