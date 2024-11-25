namespace Proj.Domain.MusicAgg.Services;

public interface IMusicDomainService
{
    bool IsSlugExist(string slug);
}