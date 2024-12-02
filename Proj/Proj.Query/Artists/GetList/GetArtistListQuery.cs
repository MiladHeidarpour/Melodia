using Common.Query;
using Proj.Query.Artists.Dtos;

namespace Proj.Query.Artists.GetList;

public record GetArtistListQuery : IQuery<List<ArtistDto>>;