using Common.Query;
using Proj.Query.MusicAlbums.Dtos;

namespace Proj.Query.MusicAlbums.GetById;

public record GetMusicAlbumByIdQuery(long AlbumId) : IQuery<MusicAlbumDto>;