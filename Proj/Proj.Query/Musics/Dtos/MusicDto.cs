using Common.Domain.ValueObjects;
using Common.Query;

namespace Proj.Query.Musics.Dtos;

public class MusicDto : BaseDto
{
    public string TrackName { get; set; }
    public string CoverImg { get; set; }
    public long CategoryId { get; set; }
    public string TrackFile { get; set; }
    public TimeSpan TrackTime { get; set; }
    public DateTime RelaseDate { get; set; }
    public string? Lyric { get; set; }
    public bool IsAlbum { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}