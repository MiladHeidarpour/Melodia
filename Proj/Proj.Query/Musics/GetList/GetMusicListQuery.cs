using Common.Query;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.Musics.GetList;

public record GetMusicListQuery : IQuery<List<MusicDto>>;