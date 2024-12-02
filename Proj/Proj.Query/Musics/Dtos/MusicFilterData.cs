using Common.Query;
using Common.Query.Filter;

namespace Proj.Query.Musics.Dtos;

public class MusicFilterData : BaseDto
{
    public string Slug { get; set; }
}
public class MusicFilterParams : BaseFilterParam
{
    public string? TrackName { get; set; }
    public long? AlbumId { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Slug { get; set; }
}
public class MusicFilterResult : BaseFilter<MusicFilterData, MusicFilterParams>
{

}