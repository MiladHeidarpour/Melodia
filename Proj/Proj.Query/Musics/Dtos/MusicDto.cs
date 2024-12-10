using Common.Domain.ValueObjects;
using Common.Query;

namespace Proj.Query.Musics.Dtos;

public class MusicDto : BaseDto
{
    public string TrackName { get; set; }
    public long AlbumId { get; set; }
    public string TrackFile { get; set; }
    public string TrackTime { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Lyric { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public List<ArtistMusicDto> ArtistMusics { get; set; }
}