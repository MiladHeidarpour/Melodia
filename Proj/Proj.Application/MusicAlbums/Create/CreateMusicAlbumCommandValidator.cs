using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.MusicAlbums.Create;

public class CreateMusicAlbumCommandValidator : AbstractValidator<CreateMusicAlbumCommand>
{
    public CreateMusicAlbumCommandValidator()
    {
        RuleFor(r => r.AlbumName).NotEmpty().WithMessage(ValidationMessages.required("نام آلبوم"));

        RuleFor(r => r.CoverImg).JustImageFile();

        RuleFor(r => r.AlbumTime).NotEmpty().WithMessage(ValidationMessages.required("مدت زمان آلبوم"));

        RuleFor(r => r.NumberOfSongs).NotEmpty().WithMessage(ValidationMessages.required("تعداد موزیک"));

        RuleFor(r => r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));
    }
}