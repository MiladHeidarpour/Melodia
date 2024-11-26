using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Application.Musics.Services;

public class MusicDomainService : IMusicDomainService
{
    private readonly IMusicRepository _musicRepository;

    public MusicDomainService(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }

    public bool IsSlugExist(string slug)
    {
        return _musicRepository.Exists(r => r.Slug == slug);
    }
}