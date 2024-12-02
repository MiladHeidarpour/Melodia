using Common.Query;
using Proj.Query.MusicAlbums.Dtos;

namespace Proj.Query.MusicAlbums.GetList;

public record GetMusicAlbumListQuery : IQuery<List<MusicAlbumDto>>;