using Common.Domain.Base;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Proj.Domain.ArtistAgg.Services;

namespace Proj.Domain.ArtistAgg;

public class Artist : AggregateRoot
{
    public string ArtistName { get; private set; }
    public string ArtistImg { get; private set; }
    public long CategoryId { get; private set; }
    public string? AboutArtist { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    private Artist()
    {

    }
    public Artist(string artistName, string artistImg, long categoryId, string? aboutArtist, string slug,
        SeoData seoData, IArtistDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(artistImg, nameof(artistImg));
        Guard(artistName, slug, domainService);
        ArtistName = artistName;
        ArtistImg = artistImg;
        CategoryId = categoryId;
        AboutArtist = aboutArtist;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }
    public void Edit(string artistName, string? aboutArtist, string slug, long categoryId,
        SeoData seoData, IArtistDomainService domainService)
    {
        Guard(artistName, slug, domainService);
        ArtistName = artistName;
        CategoryId = categoryId;
        AboutArtist = aboutArtist;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }
    public void SetArtistImg(string artistImg)
    {
        NullOrEmptyDomainDataException.CheckString(artistImg, nameof(artistImg));
        ArtistImg = ArtistImg;
    }

    //Guard
    private void Guard(string artistName, string slug, IArtistDomainService domainService)
    {
        if (slug != Slug)
        {
            if (domainService.IsSlugExist(slug.ToSlug()))
            {
                throw new SlugIsDuplicateException();
            }
        }
        NullOrEmptyDomainDataException.CheckString(artistName, nameof(artistName));
    }
}