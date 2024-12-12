using Common.Application;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.MusicAgg.Repositories;

namespace Proj.Application.Musics.AddListArtistMusics;

internal class AddListArtistMusicsCommandHandler : IBaseCommandHandler<AddListArtistMusicsCommand>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IMusicRepository _musicRepository;

    public AddListArtistMusicsCommandHandler(IMusicRepository musicRepository, IArtistRepository artistRepository)
    {
        _musicRepository = musicRepository;
        _artistRepository = artistRepository;
    }

    public async Task<OperationResult> Handle(AddListArtistMusicsCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetTracking(request.Artists.Select(f => f.ArtistId).FirstOrDefault());
        var music = await _musicRepository.GetTracking(request.Artists.Select(f => f.MusicId).FirstOrDefault());
        if (music == null)
        {
            return OperationResult.NotFound("آرتیست مورد نظر یافت نشد");
        }
        if (music == null)
        {
            return OperationResult.NotFound("موزیک مورد نظر یافت نشد");
        }
        music.SetArtistMusics(request.Artists);
        await _musicRepository.Save();
        return OperationResult.Success("عملیات با موفقیت انجام شد");
    }
}