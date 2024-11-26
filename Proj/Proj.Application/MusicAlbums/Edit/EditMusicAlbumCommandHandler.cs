using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Proj.Application._Utilities;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAgg.Services;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Application.MusicAlbums.Edit;

internal class EditMusicAlbumCommandHandler : IBaseCommandHandler<EditMusicAlbumCommand>
{
    private readonly IMusicAlbumRepository _musicAlbumRepository;
    private readonly IMusicAlbumDomainService _musicAlbumDomainService;
    private readonly IMusicDomainService _musicDomainService;
    private readonly IFileService _fileService;

    public EditMusicAlbumCommandHandler(IMusicAlbumRepository musicAlbumRepository, IMusicAlbumDomainService musicAlbumDomainService, IFileService fileService, IMusicDomainService musicDomainService)
    {
        _musicAlbumRepository = musicAlbumRepository;
        _musicAlbumDomainService = musicAlbumDomainService;
        _fileService = fileService;
        _musicDomainService = musicDomainService;
    }


    public async Task<OperationResult> Handle(EditMusicAlbumCommand request, CancellationToken cancellationToken)
    {
        var musicAlbum = await _musicAlbumRepository.GetTracking(request.AlbumId);

        if (musicAlbum == null)
            return OperationResult.NotFound("آلبوم مورد نظر یافت نشد");

        musicAlbum.Edit(request.AlbumName,request.AlbumTime,request.NumberOfSongs,request.Slug,request.SeoData,_musicAlbumDomainService);

        var oldCoverImg = musicAlbum.CoverImg;
        if (request.CoverImg != null)
        {
            var coverImg = await _fileService.SaveFileAndGenerateName(request.CoverImg, Directories.MusicAlbumCovers);
            musicAlbum.SetMusicAlbumCoverImg(coverImg);
        }

        var musics = new List<Music>();
        request.Musics.ForEach(f =>
        {
            musics.Add(new Music(
                f.TrackName,
                f.CoverImg,
                f.CategoryId,
                f.TrackFile,
                f.TrackTime,
                f.RelaseDate,
                f.Lyric,
                f.IsAlbum,
                f.Slug,
                f.SeoData,
                _musicDomainService));
        });
        musicAlbum.SetAlbumMusics(musics);

        await _musicAlbumRepository.Save();
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