using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Application.Musics.Delete;

internal class DeleteMusicCommandHandler : IBaseCommandHandler<DeleteMusicCommand>
{
    private readonly IMusicRepository _repository;
    private readonly IFileService _fileService;

    public DeleteMusicCommandHandler(IFileService fileService, IMusicRepository repository)
    {
        _repository = repository;
        _fileService = fileService;
    }


    public async Task<OperationResult> Handle(DeleteMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _repository.GetTracking(request.MusicId);
        if (music == null)
        {
            return OperationResult.NotFound("موزیک مورد نظر یافت نشد");
        }

        var result = await _repository.DeleteMusic(request.MusicId);
        if (result == true)
        {
            await _repository.Save();
            _fileService.DeleteFile(Directories.MusicFiles, music.TrackName);
            return OperationResult.Success("حذف موزیک با موفقیت انجام شد");
        }

        return OperationResult.Error("امکان حذف این موزیک وجود ندارد");
    }
}