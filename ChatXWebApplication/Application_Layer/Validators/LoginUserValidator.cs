
using ChatX.Application.DTOs.Request;
using FluentValidation;

namespace Application_Layer.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Identifier)
                .NotEmpty()
                .WithMessage("Email or username is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}