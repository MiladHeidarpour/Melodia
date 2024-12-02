using Common.Domain.ValueObjects;
using Common.Query;

namespace Proj.Query.MusicAlbums.Dtos;

public class MusicAlbumDto : BaseDto
{
    public string AlbumName { get; set; }
    public string CoverImg { get; set; }
    public TimeSpan AlbumTime { get; set; }
    public int NumberOfSongs { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}