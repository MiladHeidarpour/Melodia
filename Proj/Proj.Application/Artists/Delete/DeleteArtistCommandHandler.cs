using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg.Repositories;

namespace Proj.Application.Artists.Delete;

internal class DeleteArtistCommandHandler : IBaseCommandHandler<DeleteArtistCommand>
{
    private readonly IArtistRepository _repository;
    private readonly IFileService _fileService;

    public DeleteArtistCommandHandler(IArtistRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _repository.GetTracking(request.ArtistId);

        if (artist == null)
            return OperationResult.NotFound("آرتیست مورد نظر یافت نشد");

        var result = await _repository.DeleteArtist(request.ArtistId);

        if (result)
        {
            await _repository.Save();
            _fileService.DeleteFile(Directories.ArtistImages, artist.ArtistImg);
            return OperationResult.Success("حذف آرتیست با موفقیت انجام شد");
        }

        return OperationResult.Error("امکان حذف این آرتیست وجود ندارد");
    }
}