using Common.Query;
using Proj.Query.MusicAlbums.Dtos;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.MusicAlbums.GetByFilter;

public class GetMusicAlbumByFilterQuery:QueryFilter<MusicAlbumFilterResult,MusicAlbumFilterParams>
{
    public GetMusicAlbumByFilterQuery(MusicAlbumFilterParams filterParams) : base(filterParams)
    {
    }
}