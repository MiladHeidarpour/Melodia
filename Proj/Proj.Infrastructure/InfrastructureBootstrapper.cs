using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.CategoryAgg.Repositories;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Infrastructure.Persistent.Dapper;
using Proj.Infrastructure.Persistent.EF;
using Proj.Infrastructure.Persistent.EF.ArtistAgg;
using Proj.Infrastructure.Persistent.EF.CategoryAgg;
using Proj.Infrastructure.Persistent.EF.MusicAgg;
using Proj.Infrastructure.Persistent.EF.MusicAlbumAgg;

namespace Proj.Infrastructure;

public static class InfrastructureBootstrapper
{
    public static void Init(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IArtistRepository, ArtistRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IMusicAlbumRepository, MusicAlbumRepository>();
        services.AddTransient<IMusicRepository, MusicRepository>();

        services.AddDbContext<ProjContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });

        services.AddTransient<DapperContext>(_ =>
        {
            return new DapperContext(connectionString);
        });
    }
}