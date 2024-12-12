using Common.Query;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.Musics.ArtistMusic.GetById;

public record GetArtistMusicByIdQuery(long ArtistMusicId) : IQuery<ArtistMusicDto>;