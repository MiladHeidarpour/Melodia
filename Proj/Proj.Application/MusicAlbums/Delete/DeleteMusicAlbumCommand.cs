using Common.Application;

namespace Proj.Application.MusicAlbums.Delete;

public record DeleteMusicAlbumCommand(long AlbumId) : IBaseCommand;