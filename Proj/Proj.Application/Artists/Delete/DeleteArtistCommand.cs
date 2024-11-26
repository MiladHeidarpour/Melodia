using Common.Application;
using Proj.Domain.CategoryAgg.Repositories;

namespace Proj.Application.Artists.Delete;

public record DeleteArtistCommand(long ArtistId) : IBaseCommand;