namespace Proj.Domain.ArtistAgg.Services;

public interface IArtistDomainService
{
    bool IsSlugExist(string slug);
}