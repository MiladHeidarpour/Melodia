using Microsoft.EntityFrameworkCore;
using Proj.Domain.ArtistAgg;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.ArtistAgg;

internal class ArtistRepository : BaseRepository<Artist>, IArtistRepository
{
    public ArtistRepository(ProjContext context) : base(context)
    {
    }

    public async Task<bool> DeleteArtist(long artistId)
    {
        var artist = await _context.Artists.FirstOrDefaultAsync(f => f.Id == artistId);

        if (artist == null)
            return false;

        var isExistMusic = await _context.Musics.AnyAsync(f => f.ArtistMusics.Exists(f => f.Id == artistId));

        if (isExistMusic)
            return false;

        _context.Artists.Remove(artist);
        return true;
    }
}