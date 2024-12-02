using Common.Query;
using Proj.Query.Artists.Dtos;

namespace Proj.Query.Artists.GetById;

public record GetArtistByIdQuery(long artistId) : IQuery<ArtistDto>;