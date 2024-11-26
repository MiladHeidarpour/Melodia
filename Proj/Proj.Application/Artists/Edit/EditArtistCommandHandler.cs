using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.ArtistAgg.Services;

namespace Proj.Application.Artists.Edit;

internal class EditArtistCommandHandler : IBaseCommandHandler<EditArtistCommand>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IArtistDomainService _artistDomainService;
    private readonly IFileService _fileService;

    public EditArtistCommandHandler(IArtistRepository artistRepository, IArtistDomainService artistDomainService, IFileService fileService)
    {
        _artistRepository = artistRepository;
        _artistDomainService = artistDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetTracking(request.ArtistId);

        if (artist == null)
            return OperationResult.NotFound("آرتیست مورد نظر یافت نشد");

        artist.Edit(request.ArtistName, request.AboutArtist, request.CategoryId, request.Slug, request.SeoData, _artistDomainService);

        var oldImage = artist.ArtistImg;
        if (request.ArtistImg != null)
        {
            var artistImage = await _fileService.SaveFileAndGenerateName(request.ArtistImg, Directories.ArtistImages);
            artist.SetArtistImg(artistImage);
        }

        await _artistRepository.Save();
        RemoveOldImage(request.ArtistImg, oldImage);
        return OperationResult.Success("ویرایش آرتیست با موفقیت انجام شد");
    }

    private void RemoveOldImage(IFormFile? imageFile, string oldImageName)
    {
        if (imageFile != null)
        {
            _fileService.DeleteFile(Directories.ArtistImages, oldImageName);
        }
    }
}