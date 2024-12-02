using Common.Query;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.Musics.GetById;

public record GetMusicByIdQuery(long MusicId) : IQuery<MusicDto>;