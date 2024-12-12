using Common.Application;
using Proj.Domain.MusicAgg.Repositories;

namespace Proj.Application.Musics.DeleteArtistMusic;

internal class DeleteArtistMusicCommandHandler : IBaseCommandHandler<DeleteArtistMusicCommand>
{
    private readonly IMusicRepository _musicRepository;

    public DeleteArtistMusicCommandHandler(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }

    public async Task<OperationResult> Handle(DeleteArtistMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _musicRepository.GetTracking(request.MusicId);
        if (music == null)
        {
            return OperationResult.NotFound("موزیک مورد نظر یافت نشد");
        }
        music.DeleteArtistMusic(request.ArtistMusicId);
        await _musicRepository.Save();
        return OperationResult.Success("عملیات با موفقیت انجام شد");
    }
}