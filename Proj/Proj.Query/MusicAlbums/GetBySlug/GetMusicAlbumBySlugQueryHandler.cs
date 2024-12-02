using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.MusicAlbums.Dtos;
using Proj.Query.MusicAlbums.Mapper;

namespace Proj.Query.MusicAlbums.GetBySlug;

internal class GetMusicAlbumBySlugQueryHandler : IQueryHandler<GetMusicAlbumBySlugQuery, MusicAlbumDto>
{
    private readonly ProjContext _context;

    public GetMusicAlbumBySlugQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<MusicAlbumDto> Handle(GetMusicAlbumBySlugQuery request, CancellationToken cancellationToken)
    {
        var musicAlbum = await _context.MusicAlbums.FirstOrDefaultAsync(f => f.Slug == request.Slug, cancellationToken);
        return musicAlbum.Map();
    }
}