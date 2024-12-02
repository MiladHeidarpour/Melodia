using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Proj.Application._Utilities;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Application.Musics.Edit;

internal class EditMusicCommandHandler : IBaseCommandHandler<EditMusicCommand>
{
    private readonly IMusicRepository _repository;
    private readonly IMusicDomainService _domainService;
    private readonly IFileService _fileService;

    public EditMusicCommandHandler(IFileService fileService, IMusicRepository repository, IMusicDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _repository.GetTracking(request.MusicId);
        if (music == null)
        {
            return OperationResult.NotFound("موزیک مورد نظر یافت نشد");
        }

        music.Edit(request.TrackName, request.AlbumId, request.TrackTime,
            request.ReleaseDate, request.Lyric, request.Slug, request.SeoData, _domainService);

        var oldTrackFile = music.TrackFile;
        if (request.TrackFile != null)
        {
            var trackFile = await _fileService.SaveFileAndGenerateName(request.TrackFile, Directories.MusicFiles);
            music.SetMusicTrackFile(trackFile);
        }

        await _repository.Save();
        RemoveOldMusicFile(request.TrackFile, oldTrackFile);
        return OperationResult.Success("ویرایش موزیک با موفقیت انجام شد");
    }
    private void RemoveOldMusicFile(IFormFile musicFile, string oldMusicFile)
    {
        if (musicFile != null)
        {
            _fileService.DeleteFile(Directories.MusicFiles, oldMusicFile);
        }
    }
}