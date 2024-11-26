using Common.Application;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAgg.Services;

namespace Proj.Application.MusicAlbums.Delete;

public record DeleteMusicAlbumCommand(long AlbumId) : IBaseCommand;