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

        var result = await _repository.DeleteArtist(request.ArtistId);

        if (result == false)
        {
            return OperationResult.Error("امکان حذف این آرتیست وجود ندارد");
        }

        await _repository.Save();
        return OperationResult.Success("حذف با موفقیت انجام شد");
    }
}