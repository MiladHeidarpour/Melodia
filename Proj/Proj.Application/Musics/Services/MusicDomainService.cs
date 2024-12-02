using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Application.Musics.Services;

public class MusicDomainService : IMusicDomainService
{
    private readonly IMusicRepository _repository;

    public MusicDomainService(IMusicRepository repository)
    {
        _repository = repository;
    }

    public bool IsSlugExist(string slug)
    {
        return _repository.Exists(s => s.Slug == slug);
    }
}