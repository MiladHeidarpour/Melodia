using Common.Application;

namespace Proj.Application.Artists.Delete;

public record DeleteArtistCommand(long ArtistId) : IBaseCommand;