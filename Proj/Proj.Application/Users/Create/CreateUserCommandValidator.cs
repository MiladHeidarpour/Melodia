using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        //RuleFor(r => r.PhoneNumber)
        //    .ValidPhoneNumber();

        //RuleFor(r => r.Email)
        //    .EmailAddress().WithMessage("ایمیل نامعتبر است");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
            .NotNull()
            .MaximumLength(50).WithMessage("کلمه عبور نباید بیشتر از 50 کارکتر باشد")
            .MinimumLength(6).WithMessage("کلمه عبور باید بیشتر از 5 کارکتر باشد");
    }
}