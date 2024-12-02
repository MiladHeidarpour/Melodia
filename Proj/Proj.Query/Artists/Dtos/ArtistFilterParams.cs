using Common.Query.Filter;

namespace Proj.Query.Artists.Dtos;

public class ArtistFilterParams:BaseFilterParam
{
    public long? ArtistId { get; set; }
}