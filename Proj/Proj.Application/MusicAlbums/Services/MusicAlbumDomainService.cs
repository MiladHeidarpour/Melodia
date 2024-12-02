using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Application.MusicAlbums.Services;

public class MusicAlbumDomainService : IMusicAlbumDomainService
{

    private readonly IMusicAlbumRepository _repository;

    public MusicAlbumDomainService(IMusicAlbumRepository repository)
    {
        _repository = repository;
    }

    public bool IsSlugExist(string slug)
    {
        return _repository.Exists(s => s.Slug == slug);
    }
}