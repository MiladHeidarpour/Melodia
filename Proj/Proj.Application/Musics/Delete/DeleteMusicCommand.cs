using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Domain.ArtistAgg.Services;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Application.Musics.Delete;

public record DeleteMusicCommand(long MusicId) : IBaseCommand;

internal class DeleteMusicCommandHandler : IBaseCommandHandler<DeleteMusicCommand>
{
    private readonly IMusicRepository _musicRepository;
    private readonly IMusicDomainService _musicDomainService;
    private readonly IArtistDomainService _artistDomainService;
    private readonly IFileService _fileService;

    public DeleteMusicCommandHandler(IMusicRepository musicRepository, IMusicDomainService musicDomainService, IFileService fileService, IArtistDomainService artistDomainService)
    {
        _musicRepository = musicRepository;
        _musicDomainService = musicDomainService;
        _fileService = fileService;
        _artistDomainService = artistDomainService;
    }


    public async Task<OperationResult> Handle(DeleteMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _musicRepository.GetTracking(request.MusicId);
        if (music==null)
            return OperationResult.NotFound("موزیک موزد نظر یافت نشد");

        var result = await _musicRepository.DeleteMusic(request.MusicId);
        if (result==true)
        {
            await _musicRepository.Save();
            return OperationResult.Success("حذف موزیک با موفقیت انجام شد");
        }

        return OperationResult.Error("امکان حذف این موزیک وجود ندارد");
    }
}
