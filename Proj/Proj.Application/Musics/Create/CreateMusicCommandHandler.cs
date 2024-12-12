using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;
using Proj.Domain.MusicAlbumAgg.Repositories;

namespace Proj.Application.Musics.Create;

internal class CreateMusicCommandHandler : IBaseCommandHandler<CreateMusicCommand>
{
    private readonly IMusicRepository _repository;
    private readonly IMusicAlbumRepository _albumRepository;
    private readonly IMusicDomainService _domainService;
    private readonly IFileService _fileService;

    public CreateMusicCommandHandler(IMusicDomainService domainService, IFileService fileService, IMusicRepository repository, IMusicAlbumRepository albumRepository)
    {
        _repository = repository;
        _albumRepository = albumRepository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateMusicCommand request, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.GetTracking(request.AlbumId);
        if (album == null)
        {
            return OperationResult.NotFound("آلبوم مورد نظر یافت نشد");
        }

        var trackFile = await _fileService.SaveFileAndGenerateName(request.TrackFile, Directories.MusicFiles);

        var music = new Music(request.TrackName, request.AlbumId, trackFile, request.TrackTime, request.ReleaseDate,
            request.Lyric, request.Slug, request.SeoData, _domainService);

        await _repository.AddAsync(music);

        music.SetArtistMusics(request.ArtistMusics);
        await _repository.Save();
        return OperationResult.Success("موزیک با موفقیت ثبت شد");
    }
}