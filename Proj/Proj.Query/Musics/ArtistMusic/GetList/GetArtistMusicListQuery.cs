using Common.Query;
using Dapper;
using Proj.Infrastructure.Persistent.Dapper;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.Musics.ArtistMusic.GetList;

public record GetArtistMusicListQuery(long MusicId) : IQuery<List<ArtistMusicDto>>;

internal class GetArtistMusicListQueryHandler : IQueryHandler<GetArtistMusicListQuery, List<ArtistMusicDto>>
{
    private readonly DapperContext _dapperContext;

    public GetArtistMusicListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<ArtistMusicDto>> Handle(GetArtistMusicListQuery request, CancellationToken cancellationToken)
    {
        using var context = _dapperContext.CreateConnection();
        var sql = $"SELECT * FROM {_dapperContext.ArtistMusics} WHERE MusicId=@musicId";
        var result = await context.QueryAsync<ArtistMusicDto>(sql, new { musicId = request.MusicId });
        return result.ToList();
    }
}