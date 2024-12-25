using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proj.Domain.ArtistAgg.Repositories;
using Proj.Domain.CategoryAgg.Repositories;
using Proj.Domain.MusicAgg.Repositories;
using Proj.Domain.MusicAlbumAgg.Repositories;
using Proj.Domain.RoleAgg.Repositories;
using Proj.Domain.UserAgg.Repositories;
using Proj.Domain.VerificationAgg.Repositories;
using Proj.Infrastructure.Persistent.Dapper;
using Proj.Infrastructure.Persistent.EF;
using Proj.Infrastructure.Persistent.EF.ArtistAgg;
using Proj.Infrastructure.Persistent.EF.CategoryAgg;
using Proj.Infrastructure.Persistent.EF.MusicAgg;
using Proj.Infrastructure.Persistent.EF.MusicAlbumAgg;
using Proj.Infrastructure.Persistent.EF.RoleAgg;
using Proj.Infrastructure.Persistent.EF.UserAgg;
using Proj.Infrastructure.Persistent.EF.VerificationAgg;

namespace Proj.Infrastructure;

public static class InfrastructureBootstrapper
{
    public static void Init(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IArtistRepository, ArtistRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IMusicAlbumRepository, MusicAlbumRepository>();
        services.AddTransient<IMusicRepository, MusicRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IVerificationRepository, VerificationRepository>();

        services.AddTransient<DapperContext>(_ =>
        {
            return new DapperContext(connectionString);
        });

        services.AddDbContext<ProjContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}