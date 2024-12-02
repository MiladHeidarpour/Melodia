using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Artists.Dtos;
using Proj.Query.Artists.Mapper;

namespace Proj.Query.Artists.GetById;

internal class GetArtistByIdQueryHandler : IQueryHandler<GetArtistByIdQuery, ArtistDto>
{
    private readonly ProjContext _context;

    public GetArtistByIdQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<ArtistDto> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists.FirstOrDefaultAsync(f => f.Id == request.artistId, cancellationToken);
        return artist.Map();
    }
}