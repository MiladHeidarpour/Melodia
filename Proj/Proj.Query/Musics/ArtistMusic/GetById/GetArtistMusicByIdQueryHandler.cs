using Common.Query;
using Dapper;
using Proj.Infrastructure.Persistent.Dapper;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.Musics.ArtistMusic.GetById;

internal class GetArtistMusicByIdQueryHandler : IQueryHandler<GetArtistMusicByIdQuery, ArtistMusicDto>
{
    private readonly DapperContext _dapperContext;

    public GetArtistMusicByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<ArtistMusicDto> Handle(GetArtistMusicByIdQuery request, CancellationToken cancellationToken)
    {
        using var context = _dapperContext.CreateConnection();
        var sql = $"SELECT Top 1 * FROM {_dapperContext.ArtistMusics} WHERE id=@id";
        var result = await context.QueryFirstOrDefaultAsync<ArtistMusicDto>(sql, new { id = request.ArtistMusicId });
        return result;
    }
}