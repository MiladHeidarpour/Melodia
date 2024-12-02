using Common.Query;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.Musics.GetByFilter;

public class GetMusicByFilterQuery : QueryFilter<MusicFilterResult, MusicFilterParams>
{
    public GetMusicByFilterQuery(MusicFilterParams filterParams) : base(filterParams)
    {
    }
}