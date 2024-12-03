using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Musics.Dtos;
using Proj.Query.Musics.Mapper;

namespace Proj.Query.Musics.GetList;

internal class GetMusicListQueryHandler : IQueryHandler<GetMusicListQuery, List<MusicDto>>
{
    private readonly ProjContext _context;

    public GetMusicListQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<List<MusicDto>> Handle(GetMusicListQuery request, CancellationToken cancellationToken)
    {
        var musics = await _context.Musics.OrderByDescending(f => f.CreationDate).ToListAsync(cancellationToken);
        return musics.Map();
    }
}