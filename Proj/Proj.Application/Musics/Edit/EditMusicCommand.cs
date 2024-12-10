using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Musics.Edit;

public class EditMusicCommand : IBaseCommand
{
    public long MusicId { get; set; }
    public string TrackName { get; set; }
    public long AlbumId { get; set; }
    public IFormFile? TrackFile { get; set; }
    public string TrackTime { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Lyric { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}