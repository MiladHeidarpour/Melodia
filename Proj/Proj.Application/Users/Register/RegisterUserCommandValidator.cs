using Common.Application.Validations;
using FluentValidation;

namespace Proj.Application.Users.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
            .NotNull().MinimumLength(6).WithMessage("کلمه عبور باید بیشتر از 5 کارکتر باشد");
    }
}