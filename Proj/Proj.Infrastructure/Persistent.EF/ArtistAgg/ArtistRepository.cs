using Common.Application.FileUtil.Interfaces;
using Microsoft.EntityFrameworkCore;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.ArtistAgg;

internal class ArtistRepository : BaseRepository<Artist>, IArtistRepository
{
    private readonly IFileService _fileService;
    public ArtistRepository(ProjContext context, IFileService fileService) : base(context)
    {
        _fileService = fileService;
    }

    public async Task<bool> DeleteArtist(long artistId)
    {
        var artist = await _context.Artists.FirstOrDefaultAsync(f => f.Id == artistId);

        if (artist == null)
            return false;

        var isExistMusic = await _context.Musics.AnyAsync(f => f.ArtistMusics.Exists(f => f.Id == artistId));

        if (isExistMusic)
            return false;

        _fileService.DeleteFile(Directories.ArtistImages, artist.ArtistImg);

        _context.Artists.Remove(artist);
        return true;
    }
}