using Common.Query;
using Proj.Query.MusicAlbums.Dtos;

namespace Proj.Query.MusicAlbums.GetBySlug;

public record GetMusicAlbumBySlugQuery(string Slug) : IQuery<MusicAlbumDto>;