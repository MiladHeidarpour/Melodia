using Common.Query;
using Proj.Query.Artists.Dtos;

namespace Proj.Query.Artists.GetByFilter;

public class GetArtistByFilterQuery:QueryFilter<ArtistFilterResult,ArtistFilterParams>
{
    public GetArtistByFilterQuery(ArtistFilterParams filterParams) : base(filterParams)
    {
    }
}