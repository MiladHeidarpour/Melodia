using Common.Application;

namespace Proj.Application.Musics.Delete;

public record DeleteMusicCommand(long MusicId) : IBaseCommand;