using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.Categories.Edit;

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(r => r.Title).NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));

        RuleFor(r => r.ImageName).JustImageFile();
    }
}