using Common.Domain.Repositories;
namespace Proj.Domain.ArtistAgg.Repositories;

public interface IArtistRepository:IBaseRepository<Artist>
{
    Task<bool> DeleteArtist(long artistId);
}