using Common.Domain.Base;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAlbumAgg.Enums;
using Proj.Domain.MusicAlbumAgg.Services;

namespace Proj.Domain.MusicAlbumAgg;

public class MusicAlbum : AggregateRoot
{
    public string AlbumName { get; private set; }
    public string CoverImg { get; private set; }
    public long CategoryId { get; private set; }
    public AlbumType AlbumType { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    //public TimeSpan AlbumTime
    //{
    //    get
    //    {
    //        TimeSpan total = TimeSpan.Zero;
    //        foreach (var time in Musics.Select(f => f.TrackTime))
    //        {
    //            total += time;
    //        }

    //        return total;
    //    }
    //}
    //public int NumberOfSongs
    //{
    //    get
    //    {
    //        return Musics.Count(f => f.AlbumId == Id);
    //    }
    //}

    private MusicAlbum()
    {

    }
    public MusicAlbum(string albumName, string coverImg, long categoryId, AlbumType albumType, string slug,
        SeoData seoData, IMusicAlbumDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(coverImg, nameof(coverImg));
        Guard(albumName, slug, domainService);
        AlbumName = albumName;
        CoverImg = coverImg;
        CategoryId = categoryId;
        AlbumType = albumType;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }
    public void Edit(string albumName, long categoryId, AlbumType albumType, string slug,
        SeoData seoData, IMusicAlbumDomainService domainService)
    {
        Guard(albumName, slug, domainService);
        AlbumName = albumName;
        CategoryId = categoryId;
        AlbumType = albumType;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }
    public void SetMusicAlbumCoverImg(string coverImg)
    {
        NullOrEmptyDomainDataException.CheckString(coverImg, nameof(coverImg));
        CoverImg = coverImg;
    }

    //Guard
    private void Guard(string albumName, string slug, IMusicAlbumDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(albumName, nameof(albumName));
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