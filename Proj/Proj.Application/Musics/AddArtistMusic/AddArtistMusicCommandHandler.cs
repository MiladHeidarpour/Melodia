using Common.Application;
using FluentValidation;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAgg.Repositories;

namespace Proj.Application.Musics.AddArtistMusic;

internal class AddArtistMusicCommandHandler : IBaseCommandHandler<AddArtistMusicCommand>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IMusicRepository _musicRepository;

    public AddArtistMusicCommandHandler(IArtistRepository artistRepository, IMusicRepository musicRepository)
    {
        _artistRepository = artistRepository;
        _musicRepository = musicRepository;
    }

    public async Task<OperationResult> Handle(AddArtistMusicCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetTracking(request.ArtistId);
        var music = await _musicRepository.GetTracking(request.MusicId);

        if (artist == null)
        {
            return OperationResult.NotFound("آرتیست مورد نظر یافت نشد");
        }
        if (music == null)
        {
            return OperationResult.NotFound("موزیک مورد نظر یافت نشد");
        }

        music.SetArtistMusic(new ArtistMusic(request.ArtistId, request.MusicId, request.ArtistType));
        await _musicRepository.Save();
        return OperationResult.Success("عملیات با موفقیت ثبت شد");
    }
}