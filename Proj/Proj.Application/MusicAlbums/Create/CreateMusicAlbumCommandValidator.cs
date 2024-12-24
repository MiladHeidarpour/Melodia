using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.MusicAlbums.Create;

public class CreateMusicAlbumCommandValidator : AbstractValidator<CreateMusicAlbumCommand>
{
    public CreateMusicAlbumCommandValidator()
    {
        RuleFor(r => r.AlbumName).NotEmpty().WithMessage(ValidationMessages.required("نام آلبوم"));

        RuleFor(r => r.CategoryId).NotEmpty().WithMessage(ValidationMessages.required("دسته بندی"));

        RuleFor(r => r.CoverImg).JustImageFile();

        //RuleFor(r => r.AlbumType).NotEmpty().WithMessage(ValidationMessages.required("نوع آلبوم"));

        RuleFor(r => r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));
    }
}