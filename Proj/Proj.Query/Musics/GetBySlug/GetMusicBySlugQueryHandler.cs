using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Musics.Dtos;
using Proj.Query.Musics.Mapper;

namespace Proj.Query.Musics.GetBySlug;

internal class GetMusicBySlugQueryHandler : IQueryHandler<GetMusicBySlugQuery, MusicDto>
{
    private readonly ProjContext _context;

    public GetMusicBySlugQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<MusicDto> Handle(GetMusicBySlugQuery request, CancellationToken cancellationToken)
    {
        var music = await _context.Musics.FirstOrDefaultAsync(f => f.Slug == request.Slug, cancellationToken);
        return music.Map();
    }
}