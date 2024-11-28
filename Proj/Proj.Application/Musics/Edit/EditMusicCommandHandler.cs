using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg;
using Proj.Domain.ArtistAgg.Services;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Application.Musics.Edit;

internal class EditMusicCommandHandler : IBaseCommandHandler<EditMusicCommand>
{
    private readonly IMusicRepository _musicRepository;
    private readonly IMusicDomainService _musicDomainService;
    private readonly IArtistDomainService _artistDomainService;
    private readonly IFileService _fileService;

    public EditMusicCommandHandler(IMusicRepository musicRepository, IMusicDomainService musicDomainService, IFileService fileService, IArtistDomainService artistDomainService)
    {
        _musicRepository = musicRepository;
        _musicDomainService = musicDomainService;
        _fileService = fileService;
        _artistDomainService = artistDomainService;
    }

    public async Task<OperationResult> Handle(EditMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _musicRepository.GetTracking(request.MusicId);

        if (music == null)
            return OperationResult.NotFound("موزیک مورد نظر یافت نشد");

        music.Edit(request.TrackName, request.CategoryId, request.TrackTime, request.RelaseDate,
            request.Lyric, request.IsAlbum, request.Slug, request.SeoData, _musicDomainService);

        var oldCoverImg = music.CoverImg;
        if (request.CoverImg!=null)
        {
            var coverImg = await _fileService.SaveFileAndGenerateName(request.CoverImg, Directories.MusicCovers);
            music.SetMusicCoverImg(coverImg);
        }

        var oldTrackFile = music.TrackFile;
        if (request.TrackFile != null)
        {
            var trackFile = await _fileService.SaveFileAndGenerateName(request.TrackFile, Directories.MusicFiles);
            music.SetMusicTrackFile(trackFile);
        }

        //var artists = new List<Artist>();
        //request.Artists.ForEach(f =>
        //{
        //    artists.Add(new Artist(
        //        f.ArtistName,
        //        f.ArtistImg,
        //        f.AboutArtist,
        //        f.CategoryId,
        //        f.Slug,
        //        f.SeoData,
        //        _artistDomainService));
        //});
        //music.SetMusicArtist(artists);

        await _musicRepository.Save();
        RemoveOldImage(request.CoverImg,oldCoverImg);
        RemoveOldImage(request.TrackFile,oldTrackFile);
        return OperationResult.Success("ویرایش موزیک با موفقیت انجام شد");
    }
    private void RemoveOldImage(IFormFile imageFile, string oldImageName)
    {
        if (imageFile != null)
        {
            _fileService.DeleteFile(Directories.MusicCovers, oldImageName);
        }
    }
    private void RemoveOldMusicFile(IFormFile musicFile, string oldMusicFile)
    {
        if (musicFile != null)
        {
            _fileService.DeleteFile(Directories.MusicFiles, oldMusicFile);
        }
    }
}