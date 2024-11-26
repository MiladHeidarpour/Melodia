using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.ArtistAgg.Services;

namespace Proj.Application.Artists.Services;

public class ArtistDomainService : IArtistDomainService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistDomainService(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public bool IsSlugExsit(string slug)
    {
        return _artistRepository.Exists(a => a.Slug == slug);
    }
}