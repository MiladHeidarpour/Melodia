using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.ArtistAgg.Services;

namespace Proj.Application.Artists.Create;

internal class CreateArtistCommandHandler : IBaseCommandHandler<CreateArtistCommand>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IArtistDomainService _artistDomainService;
    private readonly IFileService _fileService;

    public CreateArtistCommandHandler(IArtistRepository artistRepository, IArtistDomainService artistDomainService, IFileService fileService)
    {
        _artistRepository = artistRepository;
        _artistDomainService = artistDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var artistImage = await _fileService.SaveFileAndGenerateName(request.ArtistImg, Directories.ArtistImages);

        var artist = new Artist(request.ArtistName, artistImage, request.AboutArtist, request.CategoryId, request.Slug,
            request.SeoData, _artistDomainService);
        
        await _artistRepository.AddAsync(artist);
        await _artistRepository.Save();
        return OperationResult.Success("آرتیست با موفقیت ثبت شد");
    }
}