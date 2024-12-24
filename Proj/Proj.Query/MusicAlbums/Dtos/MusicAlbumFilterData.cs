using Common.Query;
using Common.Query.Filter;
using Proj.Domain.MusicAlbumAgg.Enums;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.MusicAlbums.Dtos;

public class MusicAlbumFilterData:BaseDto
{
    public string Slug { get; set; }
}

public class MusicAlbumFilterParams : BaseFilterParam
{
    public string? AlbumName { get; set; }
    public AlbumType? AlbumType { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Slug { get; set; }
}

public class MusicAlbumFilterResult : BaseFilter<MusicAlbumFilterData, MusicAlbumFilterParams>
{

}