using Microsoft.EntityFrameworkCore;
using Proj.Domain.ArtistAgg;
using Proj.Domain.MusicAlbumAgg;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.MusicAlbumAgg;

internal class MusicAlbumRepository : BaseRepository<MusicAlbum>, IMusicAlbumRepository
{
    public MusicAlbumRepository(ProjContext context) : base(context)
    {
    }

    public async Task<bool> DeleteMusicAlbum(long albumId)
    {
        var musicAlbum = await _context.MusicAlbums.FirstOrDefaultAsync(f => f.Id == albumId);

        var isExistMusic = await _context.Musics.AnyAsync(f => f.AlbumId == albumId);

        if (isExistMusic)
            return false;
        if (musicAlbum == null)
            return false;

        _context.MusicAlbums.Remove(musicAlbum);
        return true;
    }
}