using Common.Query;
using Proj.Query.Artists.Dtos;

namespace Proj.Query.Artists.GetBySlug;

public record GetArtistBySlugQuery(string Slug) : IQuery<ArtistDto>;