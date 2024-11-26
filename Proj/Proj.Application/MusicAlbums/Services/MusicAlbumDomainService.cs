using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Application.MusicAlbums.Services;

public class MusicAlbumDomainService : IMusicAlbumDomainService
{
    private readonly IMusicAlbumRepository _musicAlbumRepository;
    public bool IsSlugExist(string slug)
    {
        return _musicAlbumRepository.Exists(s => s.Slug == slug);
    }
}