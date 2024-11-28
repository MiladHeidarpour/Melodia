using Common.Domain.Base;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Proj.Domain.ArtistAgg;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Domain.MusicAlbumAgg;

public class MusicAlbum : AggregateRoot
{
    public string AlbumName { get; private set; }
    public string CoverImg { get; private set; }
    public TimeSpan AlbumTime { get; private set; }
    public int NumberOfSongs { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    //public List<Music> Musics { get; private set; }

    public MusicAlbum(string albumName, string coverImg, TimeSpan albumTime, int numberOfSongs, string slug
        , SeoData seoData, IMusicAlbumDomainService musicAlbumService)
    {
        NullOrEmptyDomainDataException.CheckString(coverImg, nameof(coverImg));
        Gaurd(albumName, slug, musicAlbumService);
        AlbumName = albumName;
        CoverImg = coverImg;
        AlbumTime = albumTime;
        NumberOfSongs = numberOfSongs;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }

    public void Edit(string albumName, TimeSpan albumTime, int numberOfSongs, string slug
        , SeoData seoData, IMusicAlbumDomainService musicAlbumService)
    {
        Gaurd(albumName, slug, musicAlbumService);
        AlbumName = albumName;
        AlbumTime = albumTime;
        NumberOfSongs = numberOfSongs;
        Slug = slug?.ToSlug(); 
        SeoData = seoData;
    }
    //public void SetAlbumMusics(List<Music> musics)
    //{
    //    Musics = musics;
    //}

    public void SetMusicAlbumCoverImg(string coverImg)
    {
        NullOrEmptyDomainDataException.CheckString(coverImg, nameof(coverImg));
        CoverImg = coverImg;
    }
    public void Gaurd(string albumName, string slug, IMusicAlbumDomainService musicAlbumService)
    {
        NullOrEmptyDomainDataException.CheckString(albumName, nameof(albumName));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        if (slug != Slug)
        {
            if (musicAlbumService.IsSlugExist(slug) == true)
            {
                throw new SlugIsDuplicateException();
            }
        }
    }
}