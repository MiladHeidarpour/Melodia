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
    public TimeSpan TrackTime { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Lyric { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public EditMusicCommand(long musicId, string trackName, long albumId, IFormFile? trackFile, TimeSpan trackTime,
        DateTime releaseDate, string? lyric, string slug, SeoData seoData)
    {
        MusicId = musicId;
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