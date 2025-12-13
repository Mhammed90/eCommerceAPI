using eCommerce.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerce.Application.Validation.Authentication;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name cannot be empty");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress()
            .WithMessage("Invalid email format.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one upper case letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lower case")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one numbers")
            .Matches(@"[!@#$%^&*(),.?""""':{}|<>]").WithMessage("Password must contain at least one Special Character");
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}