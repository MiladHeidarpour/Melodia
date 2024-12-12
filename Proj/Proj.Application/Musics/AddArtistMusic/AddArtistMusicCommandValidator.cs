using Common.Application.Validations;
using FluentValidation;

namespace Proj.Application.Musics.EditArtistMusic;

public class AddArtistMusicCommandValidator : AbstractValidator<EditArtistMusicCommand>
{
    public AddArtistMusicCommandValidator()
    {
        RuleFor(r => r.ArtistId).NotEmpty().WithMessage(ValidationMessages.required("آرتیست"));

        RuleFor(r => r.MusicId).NotEmpty().WithMessage(ValidationMessages.required("موزیک"));

        RuleFor(r => r.ArtistType).NotEmpty().WithMessage(ValidationMessages.required("نوع آرتیست"));
    }
}