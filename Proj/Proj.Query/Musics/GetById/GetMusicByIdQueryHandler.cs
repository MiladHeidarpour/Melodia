using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Musics.Dtos;
using Proj.Query.Musics.Mapper;

namespace Proj.Query.Musics.GetById;

internal class GetMusicByIdQueryHandler : IQueryHandler<GetMusicByIdQuery, MusicDto>
{
    private readonly ProjContext _context;

    public GetMusicByIdQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<MusicDto> Handle(GetMusicByIdQuery request, CancellationToken cancellationToken)
    {
        var music = await _context.Musics.FirstOrDefaultAsync(f => f.Id == request.MusicId, cancellationToken);
        return music.Map();
    }
}