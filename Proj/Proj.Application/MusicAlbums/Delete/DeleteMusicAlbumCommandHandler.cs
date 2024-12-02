using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.MusicAlbumAgg.Repositories;

namespace Proj.Application.MusicAlbums.Delete;

internal class DeleteMusicAlbumCommandHandler : IBaseCommandHandler<DeleteMusicAlbumCommand>
{
    private readonly IMusicAlbumRepository _repository;
    private readonly IFileService _fileService;

    public DeleteMusicAlbumCommandHandler(IFileService fileService, IMusicAlbumRepository repository)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteMusicAlbumCommand request, CancellationToken cancellationToken)
    {
        var musicAlbum = await _repository.GetTracking(request.AlbumId);
        if (musicAlbum == null)
            return OperationResult.NotFound("آلبوم مورد نظر یافت نشد");

        var result = await _repository.DeleteMusicAlbum(request.AlbumId);
        if (result == true)
        {
            await _repository.Save();
            _fileService.DeleteFile(musicAlbum.CoverImg, Directories.MusicAlbumCovers);
            return OperationResult.Success("حذف آلبوم با موفقیت انجام شد");
        }

        return OperationResult.Error("امکان حذف این آلبوم وجود ندارد");
    }
}