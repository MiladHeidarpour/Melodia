using Common.Application.Validations;
using Common.Application.Validations.FluentValidations;
using FluentValidation;

namespace Proj.Application.Musics.Create;

public class CreateMusicCommandValidator : AbstractValidator<CreateMusicCommand>
{
    public CreateMusicCommandValidator()
    {
        RuleFor(r => r.TrackName).NotEmpty().WithMessage(ValidationMessages.required("نام موزیک"));

        RuleFor(r => r.CoverImg).JustImageFile();

        RuleFor(r => r.CategoryId).NotEmpty().WithMessage(ValidationMessages.required("دسته بندی"));

        RuleFor(r => r.TrackFile).NotEmpty().WithMessage(ValidationMessages.required("فایل موزیک"));

        RuleFor(r => r.TrackTime).NotEmpty().WithMessage(ValidationMessages.required("مدت زمان"));

        RuleFor(r => r.RelaseDate).NotEmpty().WithMessage(ValidationMessages.required("تاریخ انتشار"));

        RuleFor(r => r.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("اسلاگ"));
    }
}