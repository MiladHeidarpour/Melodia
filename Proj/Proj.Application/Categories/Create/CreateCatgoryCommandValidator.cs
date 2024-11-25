using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.Categories.Create;

public class CreateCatgoryCommandValidator:AbstractValidator<CreateCategoryCommand>
{
    public CreateCatgoryCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Slug)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));

        RuleFor(r => r.ImageName)
            .JustImageFile()
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("تصویر"));
    }
}