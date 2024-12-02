using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Proj.Application._Utilities;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Application.MusicAlbums.Edit;

internal class EditMusicAlbumCommandHandler : IBaseCommandHandler<EditMusicAlbumCommand>
{
    private readonly IMusicAlbumRepository _repository;
    private readonly IMusicAlbumDomainService _domainService;
    private readonly IFileService _fileService;

    public EditMusicAlbumCommandHandler(IFileService fileService, IMusicAlbumRepository repository, IMusicAlbumDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }


    public async Task<OperationResult> Handle(EditMusicAlbumCommand request, CancellationToken cancellationToken)
    {
        var musicAlbum = await _repository.GetTracking(request.AlbumId);

        if (musicAlbum == null)
            return OperationResult.NotFound("آلبوم مورد نظر یافت نشد");

        musicAlbum.Edit(request.AlbumName, request.CategoryId, request.AlbumType, request.Slug, request.SeoData, _domainService);

        var oldCoverImg = musicAlbum.CoverImg;
        if (request.CoverImg != null)
        {
            var coverImg = await _fileService.SaveFileAndGenerateName(request.CoverImg, Directories.MusicAlbumCovers);
            musicAlbum.SetMusicAlbumCoverImg(coverImg);
        }

        await _repository.Save();
        RemoveOldImage(request.CoverImg, oldCoverImg);
        return OperationResult.Success("ویرایش آلبوم با موفقیت انجام شد");
    }
    private void RemoveOldImage(IFormFile imageFile, string oldImageName)
    {
        if (imageFile != null)
        {
            _fileService.DeleteFile(Directories.MusicAlbumCovers, oldImageName);
        }
    }
}