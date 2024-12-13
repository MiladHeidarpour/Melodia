using Proj.Api.Infrastructure.JwtUtils;

namespace Proj.Api.Infrastructure;

public static class DependencyRegister
{
    public static void RegisterApiDependency(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
        services.AddTransient<CustomJwtValidation>();

        services.AddCors(option =>
        {
            option.AddPolicy(name: "MelodiaApi",
                builder =>
                {
                    builder.WithOrigins().AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
        });
    }
}