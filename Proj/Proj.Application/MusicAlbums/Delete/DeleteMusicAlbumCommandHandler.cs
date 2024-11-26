using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Application.MusicAlbums.Delete;

internal class DeleteMusicAlbumCommandHandler : IBaseCommandHandler<DeleteMusicAlbumCommand>
{
    private readonly IMusicAlbumRepository _musicAlbumRepository;
    private readonly IMusicAlbumDomainService _musicAlbumDomainService;
    private readonly IFileService _fileService;

    public DeleteMusicAlbumCommandHandler(IMusicAlbumRepository musicAlbumRepository, IMusicAlbumDomainService musicAlbumDomainService, IFileService fileService)
    {
        _musicAlbumRepository = musicAlbumRepository;
        _musicAlbumDomainService = musicAlbumDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteMusicAlbumCommand request, CancellationToken cancellationToken)
    {
        var musicAlbum = await _musicAlbumRepository.GetTracking(request.AlbumId);
        if (musicAlbum == null)
            return OperationResult.NotFound("آلبوم موزد نظر یافت نشد");

        var result = await _musicAlbumRepository.DeleteMusicAlbum(request.AlbumId);
        if (result == true)
        {
            await _musicAlbumRepository.Save();
            return OperationResult.Success("حذف آلبوم با موفقیت انجام شد");
        }

        return OperationResult.Error("امکان حذف این آلبوم وجود ندارد");
    }
}