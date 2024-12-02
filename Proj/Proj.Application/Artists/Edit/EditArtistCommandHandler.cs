using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.ArtistAgg.Services;

namespace Proj.Application.Artists.Edit;

internal class EditArtistCommandHandler : IBaseCommandHandler<EditArtistCommand>
{
    private readonly IArtistRepository _repository;
    private readonly IArtistDomainService _domainService;
    private readonly IFileService _fileService;

    public EditArtistCommandHandler(IArtistRepository repository, IArtistDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _repository.GetTracking(request.ArtistId);

        if (artist == null)
            return OperationResult.NotFound("آرتیست مورد نظر یافت نشد");

        artist.Edit(request.ArtistName, request.AboutArtist, request.Slug, request.CategoryId, request.SeoData, _domainService);

        var oldImage = artist.ArtistImg;
        if (request.ArtistImg != null)
        {
            var artistImage = await _fileService.SaveFileAndGenerateName(request.ArtistImg, Directories.ArtistImages);
            artist.SetArtistImg(artistImage);
        }

        await _repository.Save();
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