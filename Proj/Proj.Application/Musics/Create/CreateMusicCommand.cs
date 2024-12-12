using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Proj.Domain.MusicAgg;

namespace Proj.Application.Musics.Create;

public class CreateMusicCommand : IBaseCommand
{
    public string TrackName { get; set; }
    public long AlbumId { get; set; }
    public IFormFile TrackFile { get; set; }
    public string TrackTime { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Lyric { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public List<ArtistMusic> ArtistMusics { get; set; }
}