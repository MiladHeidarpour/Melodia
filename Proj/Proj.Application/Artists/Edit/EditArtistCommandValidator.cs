using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.Artists.Edit;

public class EditArtistCommandValidator : AbstractValidator<EditArtistCommand>
{
    public EditArtistCommandValidator()
    {
        RuleFor(r => r.ArtistName).NotEmpty().WithMessage(ValidationMessages.required("نام آرتیست"));

        RuleFor(r => r.CategoryId).NotEmpty().WithMessage(ValidationMessages.required("دسته بندی"));

        RuleFor(r => r.ArtistImg).JustImageFile();

        RuleFor(r => r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));
    }
}