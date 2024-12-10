using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.ArtistAgg.Services;

namespace Proj.Application.Artists.Create;

internal class CreateArtistCommandHandler : IBaseCommandHandler<CreateArtistCommand>
{
    private readonly IArtistRepository _repository;
    private readonly IArtistDomainService _domainService;
    private readonly IFileService _fileService;

    public CreateArtistCommandHandler(IArtistRepository repository, IArtistDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var artistImage = await _fileService.SaveFileAndGenerateName(request.ArtistImg, Directories.ArtistImages);

        var artist = new Artist(request.ArtistName, artistImage, request.CategoryId, request.AboutArtist, request.Slug, request.SeoData, _domainService);

        await _repository.AddAsync(artist);
        await _repository.Save();
        return OperationResult.Success("آرتیست با موفقیت ثبت شد");
    }
}