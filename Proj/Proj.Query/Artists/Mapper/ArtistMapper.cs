using Proj.Domain.ArtistAgg;
using Proj.Query.Artists.Dtos;
using Proj.Query.Categories.Dtos;

namespace Proj.Query.Artists.Mapper;

internal static class ArtistMapper
{
    public static ArtistDto? Map(this Artist? artist)
    {
        if (artist == null)
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

    public static List<ArtistDto> Map(this List<Artist> artists)
    {
        var model = new List<ArtistDto>();
        artists.ForEach(artist =>
        {
            model.Add(new ArtistDto()
            {
                Id = artist.Id,
                CreationDate = artist.CreationDate,
                ArtistName = artist.ArtistName,
                ArtistImg = artist.ArtistImg,
                AboutArtist = artist.AboutArtist,
                CategoryId = artist.CategoryId,
                Slug = artist.Slug,
                SeoData = artist.SeoData,
            });
        });
        return model;
    }
}