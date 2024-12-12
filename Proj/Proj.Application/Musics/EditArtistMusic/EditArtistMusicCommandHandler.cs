using Common.Application;
using Proj.Domain.MusicAgg.Repositories;

namespace Proj.Application.Musics.EditArtistMusic;

internal class EditArtistMusicCommandHandler : IBaseCommandHandler<EditArtistMusicCommand>
{
    private readonly IMusicRepository _musicRepository;

    public EditArtistMusicCommandHandler(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }

    public async Task<OperationResult> Handle(EditArtistMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _musicRepository.GetTracking(request.MusicId);
        if (music == null)
        {
            return OperationResult.NotFound("موزیک مورد نظر یافت نشد");
        }
        music.EditArtistMusic(request.ArtistMusicId, request.ArtistId, request.MusicId, request.ArtistType);
        await _musicRepository.Save();
        return OperationResult.Success("عملیات با موفقیت ثبت شد");
    }
}