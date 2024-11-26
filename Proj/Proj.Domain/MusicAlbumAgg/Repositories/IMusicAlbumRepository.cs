using Common.Domain.Repositories;

namespace Proj.Domain.MusicAlbumAgg.Repositories;

public interface IMusicAlbumRepository:IBaseRepository<MusicAlbum>
{
    Task<bool> DeleteMusicAlbum(long albumId);
}