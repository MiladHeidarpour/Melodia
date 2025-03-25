using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Musics.Dtos;
using Proj.Query.Musics.Mapper;

namespace Proj.Query.Musics.NewReleaseSongs;

public record NewReleaseSongsQuery : IQuery<List<MusicDto>>;

internal class NewReleaseSongsQueryHandler : IQueryHandler<NewReleaseSongsQuery, List<MusicDto>>
{
    private readonly ProjContext _context;

    public NewReleaseSongsQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<List<MusicDto>> Handle(NewReleaseSongsQuery request, CancellationToken cancellationToken)
    {
        var newRelease = await _context.Musics.OrderByDescending(c => c.CreationDate).ToListAsync(cancellationToken);
        return newRelease.Map();
    }
}