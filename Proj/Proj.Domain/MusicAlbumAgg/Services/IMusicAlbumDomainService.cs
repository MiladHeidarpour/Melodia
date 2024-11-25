namespace Proj.Domain.MusicAlbumAgg.Services;

public interface IMusicAlbumDomainService
{
    bool IsSlugExist(string slug);
}