using Common.Domain.ValueObjects;
using Common.Query;

namespace Proj.Query.Artists.Dtos;

public class ArtistDto : BaseDto
{
    public string ArtistName { get;  set; }
    public string ArtistImg { get;  set; }
    public long CategoryId { get;  set; }
    public string? AboutArtist { get;  set; }
    public string Slug { get;  set; }
    public SeoData SeoData { get;  set; }
}