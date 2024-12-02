using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.MusicAlbums.Dtos;
using Proj.Query.MusicAlbums.Mapper;

namespace Proj.Query.MusicAlbums.GetList;

internal class GetMusicAlbumListQueryHandler : IQueryHandler<GetMusicAlbumListQuery, List<MusicAlbumDto>>
{
    private readonly ProjContext _context;

    public GetMusicAlbumListQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<List<MusicAlbumDto>> Handle(GetMusicAlbumListQuery request, CancellationToken cancellationToken)
    {
        var musicAlbums = await _context.MusicAlbums.OrderByDescending(f => f.CreationDate).ToListAsync(cancellationToken);
        return musicAlbums.Map();
    }
}