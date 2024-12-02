using Common.Application.Validations;
using FluentValidation;

namespace Proj.Application.Musics.Create;

public class CreateMusicCommandValidator : AbstractValidator<CreateMusicCommand>
{
    public CreateMusicCommandValidator()
    {
        RuleFor(r => r.TrackName).NotEmpty().WithMessage(ValidationMessages.required("نام موزیک"));

        RuleFor(r => r.AlbumId).NotEmpty().WithMessage(ValidationMessages.required("آلبوم"));

        RuleFor(r => r.TrackFile).NotEmpty().WithMessage(ValidationMessages.required("فایل موزیک"));

        RuleFor(r => r.TrackTime).NotEmpty().WithMessage(ValidationMessages.required("مدت زمان"));

        RuleFor(r => r.ReleaseDate).NotEmpty().WithMessage(ValidationMessages.required("تاریخ انتشار"));

        RuleFor(r => r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));
    }
}