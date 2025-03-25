using Common.Domain.Repositories;

namespace Proj.Domain.MusicAgg.Repositories;

public interface IMusicRepository : IBaseRepository<Music>
{
    Task<bool> DeleteMusic(long musicId);
    Task<List<Music>> GetAlbumMusics(long albumId);
}