using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg;
using Proj.Domain.ArtistAgg.Services;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Application.Musics.Create;

internal class CreateMusicCommandHandler : IBaseCommandHandler<CreateMusicCommand>
{
    private readonly IMusicRepository _musicRepository;
    private readonly IMusicDomainService _musicDomainService;
    private readonly IArtistDomainService _artistDomainService;
    private readonly IFileService _fileService;

    public CreateMusicCommandHandler(IMusicRepository musicRepository, IMusicDomainService musicDomainService, IFileService fileService, IArtistDomainService artistDomainService)
    {
        _musicRepository = musicRepository;
        _musicDomainService = musicDomainService;
        _fileService = fileService;
        _artistDomainService = artistDomainService;
    }

    public async Task<OperationResult> Handle(CreateMusicCommand request, CancellationToken cancellationToken)
    {
        var coverImg = await _fileService.SaveFileAndGenerateName(request.CoverImg, Directories.MusicCovers);
        var trackFile = await _fileService.SaveFileAndGenerateName(request.TrackFile, Directories.MusicFiles);
        var music = new Music(request.TrackName, coverImg, request.CategoryId, trackFile, request.TrackTime,
            request.RelaseDate, request.Lyric, request.IsAlbum, request.Slug, request.SeoData, _musicDomainService);

        await _musicRepository.AddAsync(music);

        var artists = new List<Artist>();
        request.Artists.ForEach(f =>
        {
            artists.Add(new Artist(
                f.ArtistName,
                f.ArtistImg,
                f.AboutArtist,
                f.CategoryId,
                f.Slug,
                f.SeoData,
                _artistDomainService));
        });
        music.SetMusicArtist(artists);
        
        await _musicRepository.Save();
        return OperationResult.Success("موزیک با موفقیت ثبت شد");
    }
}