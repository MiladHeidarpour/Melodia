using Common.Domain.Base;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Proj.Domain.ArtistAgg;
using Proj.Domain.CategoryAgg.Services;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Domain.MusicAgg;

public class Music : AggregateRoot
{
    public string TrackName { get; private set; }
    public string CoverImg { get; private set; }
    public long CategoryId { get; private set; }
    public string TrackFile { get; private set; }
    public TimeSpan TrackTime { get; private set; }
    public DateTime RelaseDate { get; private set; }
    public string? Lyric { get; private set; }
    public bool IsAlbum { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<Artist> Artists { get; private set; }

    private Music()
    {
        Artists = new List<Artist>();
    }
    public Music(string trackName, string coverImg, long categoryId, string trackFile,
        TimeSpan trackTime, DateTime relaseDate, string? lyric, bool isAlbum,
        string slug, SeoData seoData, IMusicDomainService musicService)
    {
        NullOrEmptyDomainDataException.CheckString(coverImg, nameof(coverImg));
        NullOrEmptyDomainDataException.CheckString(trackFile, nameof(trackFile));
        Gaurd(trackName, slug, musicService);
        TrackName = trackName;
        CoverImg = coverImg;
        CategoryId = categoryId;
        TrackFile = trackFile;
        TrackTime = trackTime;
        RelaseDate = relaseDate;
        Lyric = lyric;
        IsAlbum = isAlbum;
        Slug = slug?.ToSlug();
        SeoData = seoData;
        Artists = new List<Artist>();
    }

    public void Edit(string trackName, long categoryId,
        TimeSpan trackTime, DateTime relaseDate, string? lyric, bool isAlbum,
        string slug, SeoData seoData, IMusicDomainService musicService)
    {
        Gaurd(trackName, slug, musicService);
        TrackName = trackName;
        CategoryId = categoryId;
        TrackTime = trackTime;
        RelaseDate = relaseDate;
        Lyric = lyric;
        IsAlbum = isAlbum;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }
    public void SetMusicCoverImg(string coverImg)
    {
        NullOrEmptyDomainDataException.CheckString(coverImg, nameof(coverImg));
        CoverImg = coverImg;
    }

    public void SetMusicTrackFile(string trackFile)
    {
        NullOrEmptyDomainDataException.CheckString(trackFile, nameof(trackFile));
        TrackFile = trackFile;
    }

    public void Gaurd(string trackName, string slug, IMusicDomainService musicService)
    {
        NullOrEmptyDomainDataException.CheckString(trackName, nameof(trackName));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        if (slug != Slug)
        {
            if (musicService.IsSlugExist(slug) == true)
            {
                throw new SlugIsDuplicateException();
            }
        }
    }
}