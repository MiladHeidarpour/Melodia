using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.MusicAlbums.Dtos;
using Proj.Query.MusicAlbums.Mapper;

namespace Proj.Query.MusicAlbums.GetById;

internal class GetMusicAlbumByIdQueryHandler : IQueryHandler<GetMusicAlbumByIdQuery, MusicAlbumDto>
{
    private readonly ProjContext _context;

    public GetMusicAlbumByIdQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<MusicAlbumDto> Handle(GetMusicAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        var musicAlbum = await _context.MusicAlbums.FirstOrDefaultAsync(f => f.Id == request.AlbumId, cancellationToken);
        return musicAlbum.Map();
    }
}