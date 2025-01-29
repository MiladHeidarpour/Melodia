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
        var result = await _repository.DeleteMusicAlbum(request.AlbumId);
        if (result == false)
        {
            return OperationResult.Error("امکان حذف این آلبوم وجود ندارد");
        }
        await _repository.Save();
        return OperationResult.Success("حذف آلبوم با موفقیت انجام شد");
    }
}