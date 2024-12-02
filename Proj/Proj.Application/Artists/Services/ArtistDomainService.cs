using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.ArtistAgg.Services;

namespace Proj.Application.Artists.Services;

public class ArtistDomainService : IArtistDomainService
{
    private readonly IArtistRepository _repository;

    public ArtistDomainService(IArtistRepository repository)
    {
        _repository = repository;
    }

    public bool IsSlugExist(string slug)
    {
        return _repository.Exists(a => a.Slug == slug);
    }
}