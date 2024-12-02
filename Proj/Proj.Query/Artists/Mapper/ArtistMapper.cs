using Proj.Domain.ArtistAgg;
using Proj.Query.Artists.Dtos;

namespace Proj.Query.Artists.Mapper;

internal static class ArtistMapper
{
    public static ArtistDto? Map(this Artist? artist)
    {
        if (artist==null)
        {
            return null;
        }

        return new ArtistDto()
        {
            Id = artist.Id,
            CreationDate = artist.CreationDate,
            ArtistName = artist.ArtistName,
            ArtistImg = artist.ArtistImg,
            AboutArtist = artist.AboutArtist,
            CategoryId = artist.CategoryId,
            Slug = artist.Slug,
            SeoData = artist.SeoData,
        };
    }
}