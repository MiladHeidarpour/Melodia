using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.ArtistAgg.Services;

namespace Proj.Application.Artists.Delete;

internal class DeleteArtistCommandHandler : IBaseCommandHandler<DeleteArtistCommand>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IArtistDomainService _artistDomainService;
    private readonly IFileService _fileService;

    public DeleteArtistCommandHandler(IArtistRepository artistRepository, IArtistDomainService artistDomainService, IFileService fileService)
    {
        _artistRepository = artistRepository;
        _artistDomainService = artistDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetTracking(request.ArtistId);

        if (artist == null)
            return OperationResult.NotFound("آرتیست مورد نظر یافت نشد");

        var result = await _artistRepository.DeleteArtist(request.ArtistId);

        if (result == true)
        {
            await _artistRepository.Save();
            return OperationResult.Success("حذف آرتیست با موفقیت انجام شد");
        }

        return OperationResult.Error("امکان حذف این آرتیست وجود ندارد");
    }
}