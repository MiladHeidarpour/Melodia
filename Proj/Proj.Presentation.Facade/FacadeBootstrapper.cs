using Microsoft.Extensions.DependencyInjection;
using Proj.Presentation.Facade.Artists;
using Proj.Presentation.Facade.Categories;
using Proj.Presentation.Facade.MusicAlbums;
using Proj.Presentation.Facade.Musics;

namespace Proj.Presentation.Facade;

public static class FacadeBootstrapper
{
    public static void InitFacadeDependency(this IServiceCollection services)
    {
        services.AddScoped<IArtistFacade, ArtistFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<IMusicAlbumFacade, MusicAlbumFacade>();
        services.AddScoped<IMusicFacade, MusicFacade>();
    }
}