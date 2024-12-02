using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Artists.Dtos;
using Proj.Query.Artists.Mapper;

namespace Proj.Query.Artists.GetBySlug;

internal class GetArtistBySlugQueryHandler : IQueryHandler<GetArtistBySlugQuery, ArtistDto>
{
    private readonly ProjContext _context;

    public GetArtistBySlugQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<ArtistDto> Handle(GetArtistBySlugQuery request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists.FirstOrDefaultAsync(f => f.Slug == request.Slug, cancellationToken);
        return artist.Map();
    }
}