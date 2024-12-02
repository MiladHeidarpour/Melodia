using Common.Query;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.Musics.GetBySlug;

public record GetMusicBySlugQuery(string Slug) : IQuery<MusicDto>;