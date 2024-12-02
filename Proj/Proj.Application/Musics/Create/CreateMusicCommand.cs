using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Musics.Create;

public class CreateMusicCommand : IBaseCommand
{
    public string TrackName { get; set; }
    public long AlbumId { get; set; }
    public IFormFile TrackFile { get; set; }
    public TimeSpan TrackTime { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Lyric { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }

    public CreateMusicCommand(string trackName, long albumId, IFormFile trackFile, TimeSpan trackTime, DateTime releaseDate, string? lyric, string slug, SeoData seoData)
    {
        TrackName = trackName;
        AlbumId = albumId;
        TrackFile = trackFile;
        TrackTime = trackTime;
        ReleaseDate = releaseDate;
        Lyric = lyric;
        Slug = slug;
        SeoData = seoData;
    }
}