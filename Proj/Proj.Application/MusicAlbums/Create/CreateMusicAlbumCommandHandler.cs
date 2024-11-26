using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAgg.Services;
using Proj.Domain.MusicAlbumAgg;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Application.MusicAlbums.Create;

internal class CreateMusicAlbumCommandHandler : IBaseCommandHandler<CreateMusicAlbumCommand>
{
    private readonly IMusicAlbumRepository _musicAlbumRepository;
    private readonly IMusicAlbumDomainService _musicAlbumDomainService;
    private readonly IMusicDomainService _musicDomainService;
    private readonly IFileService _fileService;

    public CreateMusicAlbumCommandHandler(IMusicAlbumRepository musicAlbumRepository, IMusicAlbumDomainService musicAlbumDomainService, IFileService fileService, IMusicDomainService musicDomainService)
    {
        _musicAlbumRepository = musicAlbumRepository;
        _musicAlbumDomainService = musicAlbumDomainService;
        _fileService = fileService;
        _musicDomainService = musicDomainService;
    }

    public async Task<OperationResult> Handle(CreateMusicAlbumCommand request, CancellationToken cancellationToken)
    {
        var coverImg = await _fileService.SaveFileAndGenerateName(request.CoverImg, Directories.MusicAlbumCovers);
        var musicAlbum = new MusicAlbum(request.AlbumName, coverImg,request.AlbumTime,request.NumberOfSongs,request.Slug,request.SeoData,_musicAlbumDomainService);

        await _musicAlbumRepository.AddAsync(musicAlbum);

        var musics = new List<Music>();
        request.Musics.ForEach(f =>
        {
            musics.Add(new Music(
                f.TrackName,
                f.CoverImg,
                f.CategoryId,
                f.TrackFile,
                f.TrackTime,
                f.RelaseDate,
                f.Lyric,
                f.IsAlbum,
                f.Slug,
                f.SeoData,
                _musicDomainService));
        });
        musicAlbum.SetAlbumMusics(musics);

        await _musicAlbumRepository.Save();
        return OperationResult.Success("موزیک با موفقیت ثبت شد");
    }
}