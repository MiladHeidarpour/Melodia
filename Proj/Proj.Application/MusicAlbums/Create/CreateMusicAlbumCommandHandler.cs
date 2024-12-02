using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.MusicAlbumAgg;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Application.MusicAlbums.Create;

internal class CreateMusicAlbumCommandHandler : IBaseCommandHandler<CreateMusicAlbumCommand>
{
    private readonly IMusicAlbumRepository _repository;
    private readonly IMusicAlbumDomainService _domainService;
    private readonly IFileService _fileService;

    public CreateMusicAlbumCommandHandler(IMusicAlbumRepository repository, IMusicAlbumDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateMusicAlbumCommand request, CancellationToken cancellationToken)
    {
        var coverImg = await _fileService.SaveFileAndGenerateName(request.CoverImg, Directories.MusicAlbumCovers);
        var musicAlbum = new MusicAlbum(request.AlbumName, coverImg, request.CategoryId, request.AlbumType, request.Slug, request.SeoData, _domainService);

        await _repository.AddAsync(musicAlbum);
        await _repository.Save();
        return OperationResult.Success("آلبوم با موفقیت ثبت شد");
    }
}