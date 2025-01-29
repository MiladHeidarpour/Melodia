using Common.Application.FileUtil.Interfaces;
using Microsoft.EntityFrameworkCore;
using Proj.Application._Utilities;
using Proj.Domain.MusicAlbumAgg;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.MusicAlbumAgg;

internal class MusicAlbumRepository : BaseRepository<MusicAlbum>, IMusicAlbumRepository
{
    private readonly IFileService _fileService;
    public MusicAlbumRepository(ProjContext context, IFileService fileService) : base(context)
    {
        _fileService = fileService;
    }

    public async Task<bool> DeleteMusicAlbum(long albumId)
    {
        var musicAlbum = await _context.MusicAlbums.FirstOrDefaultAsync(f => f.Id == albumId);

        if (musicAlbum == null)
            return false;

        var isExistMusic = await _context.Musics.AnyAsync(f => f.AlbumId == albumId);

        if (isExistMusic)
            return false;


        _context.MusicAlbums.Remove(musicAlbum);
        _fileService.DeleteFile(Directories.MusicAlbumCovers, musicAlbum.CoverImg);
        return true;
    }
}