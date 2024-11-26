using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Proj.Domain.ArtistAgg;

namespace Proj.Application.Musics.Create;

public class CreateMusicCommand : IBaseCommand
{
    public string TrackName { get; private set; }
    public IFormFile CoverImg { get; private set; }
    public long CategoryId { get; private set; }
    public IFormFile TrackFile { get; private set; }
    public TimeSpan TrackTime { get; private set; }
    public DateTime RelaseDate { get; private set; }
    public string? Lyric { get; private set; }
    public bool IsAlbum { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<Artist> Artists { get; private set; }

    public CreateMusicCommand(string trackName, IFormFile coverImg, long categoryId, IFormFile trackFile, TimeSpan trackTime, DateTime relaseDate, string? lyric, bool isAlbum, string slug, SeoData seoData, List<Artist> artists)
    {
        TrackName = trackName;
        CoverImg = coverImg;
        CategoryId = categoryId;
        TrackFile = trackFile;
        TrackTime = trackTime;
        RelaseDate = relaseDate;
        Lyric = lyric;
        IsAlbum = isAlbum;
        Slug = slug;
        SeoData = seoData;
        Artists = artists;
    }
}