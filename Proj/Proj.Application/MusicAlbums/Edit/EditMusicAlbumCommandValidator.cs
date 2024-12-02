using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.MusicAlbums.Edit;

public class EditMusicAlbumCommandValidator : AbstractValidator<EditMusicAlbumCommand>
{
    public EditMusicAlbumCommandValidator()
    {
        RuleFor(r => r.AlbumName).NotEmpty().WithMessage(ValidationMessages.required("نام آلبوم"));

        RuleFor(r => r.CoverImg).JustImageFile();

        RuleFor(r => r.AlbumType).NotEmpty().WithMessage(ValidationMessages.required("نوع آلبوم"));

        RuleFor(r => r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));
    }
}