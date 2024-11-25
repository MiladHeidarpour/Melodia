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
    public string? AboutArtist { get; private set; }
    public long CategoryId { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }

    private Artist()
    {

    }

    public Artist(string artistName, string artistImg, string? aboutArtist, long categoryId, string slug,
        SeoData seoData, IArtistDomainService artistService)
    {
        NullOrEmptyDomainDataException.CheckString(artistImg, nameof(artistImg));
        Gaurd(artistName, slug, artistService);
        ArtistName = artistName;
        ArtistImg = artistImg;
        AboutArtist = aboutArtist;
        CategoryId = categoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }

    public void Edit(string artistName, string? aboutArtist, long categoryId, string slug,
        SeoData seoData, IArtistDomainService artistService)
    {
        Gaurd(artistName, slug, artistService);
        ArtistName = artistName;
        AboutArtist = aboutArtist;
        CategoryId = categoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }

    public void SetArtistImg(string artistImg)
    {
        NullOrEmptyDomainDataException.CheckString(artistImg, nameof(artistImg));
        ArtistImg = ArtistImg;
    }

    public void Gaurd(string artistName, string slug, IArtistDomainService artistService)
    {
        if (slug != Slug)
        {
            if (artistService.IsSlugExsit(slug.ToSlug()) == true)
            {
                throw new SlugIsDuplicateException();
            }
        }
        NullOrEmptyDomainDataException.CheckString(artistName, nameof(artistName));
    }
}