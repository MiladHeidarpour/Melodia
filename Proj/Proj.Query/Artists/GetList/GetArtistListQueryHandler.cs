using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Artists.Dtos;
using Proj.Query.Artists.Mapper;

namespace Proj.Query.Artists.GetList;

internal class GetArtistListQueryHandler : IQueryHandler<GetArtistListQuery, List<ArtistDto>>
{
    private readonly ProjContext _context;

    public GetArtistListQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<List<ArtistDto>> Handle(GetArtistListQuery request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists.OrderByDescending(f => f.CreationDate).ToListAsync(cancellationToken);
        return artist.Map();
    }
}