using Common.Application.Validations;
using FluentValidation;
using Proj.Application.Musics.EditArtistMusic;

namespace Proj.Application.Musics.AddArtistMusic;

public class AddArtistMusicCommandValidator : AbstractValidator<EditArtistMusicCommand>
{
    public AddArtistMusicCommandValidator()
    {
        RuleFor(r => r.ArtistId).NotEmpty().WithMessage(ValidationMessages.required("آرتیست"));

        RuleFor(r => r.MusicId).NotEmpty().WithMessage(ValidationMessages.required("موزیک"));

        RuleFor(r => r.ArtistType).NotEmpty().WithMessage(ValidationMessages.required("نوع آرتیست"));
    }
}