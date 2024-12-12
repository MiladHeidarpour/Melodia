using Common.Domain.Base;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Proj.Domain.MusicAgg.Enums;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Domain.MusicAgg;
public class Music : AggregateRoot
{
    public string TrackName { get; private set; }
    public long AlbumId { get; internal set; }
    public string TrackFile { get; private set; }
    public string TrackTime { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string? Lyric { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<ArtistMusic> ArtistMusics { get; private set; }

    private Music()
    {
    }

    public Music(string trackName, long albumId, string trackFile, string trackTime, DateTime releaseDate,
        string? lyric, string slug, SeoData seoData, IMusicDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(trackFile, nameof(trackFile));
        Guard(trackName, slug, domainService);
        TrackName = trackName;
        AlbumId = albumId;
        TrackFile = trackFile;
        TrackTime = trackTime;
        ReleaseDate = releaseDate;
        Lyric = lyric;
        Slug = slug?.ToSlug();
        SeoData = seoData;
        ArtistMusics = new();
    }

    public void Edit(string trackName, long albumId, string trackTime, DateTime releaseDate, string? lyric,
        string slug, SeoData seoData, IMusicDomainService domainService)
    {
        Guard(trackName, slug, domainService);
        TrackName = trackName;
        AlbumId = albumId;
        TrackTime = trackTime;
        ReleaseDate = releaseDate;
        Lyric = lyric;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }
    public void SetMusicTrackFile(string trackFile)
    {
        NullOrEmptyDomainDataException.CheckString(trackFile, nameof(trackFile));
        TrackFile = trackFile;
    }
    public void SetArtistMusics(List<ArtistMusic> artistMusics)
    {
        ArtistMusics = artistMusics;
    }
    public void SetArtistMusic(ArtistMusic artistMusic)
    {
        artistMusic.MusicId = Id;
        ArtistMusics.Add(artistMusic);
    }

    public void EditArtistMusic(long artistMusicId, long artistId, long musicId, ArtistType artistType)
    {
        var current = ArtistMusics.FirstOrDefault(f => f.Id == artistMusicId);
        if (current == null)
        {
            return;
        }

        current.Edit(artistId, musicId, artistType);
    }

    public void DeleteArtistMusic(long artistMusicId)
    {
        var current = ArtistMusics.FirstOrDefault(f => f.Id == artistMusicId);
        if (current == null)
        {
            return;
        }

        ArtistMusics.Remove(current);
    }

    //Music 
    //public void AddMusic(Music music)
    //{
    //    music.AlbumId = Id;
    //    Musics.Add(music);
    //}
    //public void AddListMusic(List<Music> musics)
    //{
    //    musics.ForEach(f => f.AlbumId = Id);
    //    Musics = musics;
    //}
    //public void EditMusic(Music music, long musicId, IMusicAlbumDomainService domainService)
    //{
    //    var oldMusic = Musics.FirstOrDefault(f => f.Id == musicId);
    //    if (oldMusic == null)
    //    {
    //        throw new NullOrEmptyDomainDataException("موزیک مورد نظر یافت نشد");
    //    }
    //    oldMusic.Edit(music.TrackName, music.AlbumId, music.TrackTime, music.ReleaseDate, music.Lyric, music.Slug, music.SeoData, domainService);
    //}
    //public string RemoveMusic(long musicId)
    //{
    //    var music = Musics.FirstOrDefault(f => f.Id == musicId);
    //    if (music != null)
    //    {
    //        throw new NullOrEmptyDomainDataException("موزیک مورد نظر یافت نشد");
    //    }

    //    Musics.Remove(music);
    //    return music.TrackFile;
    //}

    //ArtistMusics
    //public void SetArtistMusic(List<ArtistMusic> artistMusics, long musicId)
    //{
    //    var music = Musics.FirstOrDefault(f => f.Id == musicId);
    //    artistMusics.ForEach(f => f.MusicId = Id);
    //    music.SetArtistMusics(artistMusics);
    //}

    //public void AddArtistMusic(ArtistMusic artistMusic, long musicId)
    //{
    //    var music = Musics.FirstOrDefault(f => f.Id == musicId);
    //    artistMusic.MusicId = musicId;
    //    music.SetArtistMusic(artistMusic);
    //}
    private void Guard(string trackName, string slug, IMusicDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(trackName, nameof(trackName));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        if (slug != Slug)
        {
            if (domainService.IsSlugExist(slug))
            {
                throw new SlugIsDuplicateException();
            }
        }
    }
}