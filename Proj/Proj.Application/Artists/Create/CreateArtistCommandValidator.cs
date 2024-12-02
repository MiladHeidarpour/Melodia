using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.Artists.Create;

public class CreateArtistCommandValidator : AbstractValidator<CreateArtistCommand>
{
    public CreateArtistCommandValidator()
    {
        RuleFor(r => r.ArtistName).NotEmpty().WithMessage(ValidationMessages.required("نام آرتیست"));

        RuleFor(r => r.CategoryId).NotEmpty().WithMessage(ValidationMessages.required("دسته بندی"));

        RuleFor(r => r.ArtistImg).JustImageFile()
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("تصویر")); ;

        RuleFor(r => r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));
    }
}