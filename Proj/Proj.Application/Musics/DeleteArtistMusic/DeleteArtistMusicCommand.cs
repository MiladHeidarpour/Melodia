using Common.Application;

namespace Proj.Application.Musics.DeleteArtistMusic;

public record DeleteArtistMusicCommand(long MusicId, long ArtistMusicId):IBaseCommand;