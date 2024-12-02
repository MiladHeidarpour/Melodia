using Microsoft.EntityFrameworkCore;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.MusicAgg;

internal class MusicRepository : BaseRepository<Music>, IMusicRepository
{
    public MusicRepository(ProjContext context) : base(context)
    {
    }

    public async Task<bool> DeleteMusic(long musicId)
    {
        var music = await _context.Musics.FirstOrDefaultAsync(f => f.Id == musicId);

        if (music == null)
            return false;

        _context.Musics.Remove(music);
        return true;
    }
}