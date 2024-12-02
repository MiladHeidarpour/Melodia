using Common.Domain.Repositories;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAlbumAgg;

namespace Proj.Domain.MusicAlbumAgg.Repositories;

public interface IMusicAlbumRepository : IBaseRepository<MusicAlbum>
{
    Task<bool> DeleteMusicAlbum(long albumId);
}