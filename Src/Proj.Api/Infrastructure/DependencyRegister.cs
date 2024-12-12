namespace Proj.Api.Infrastructure;

public static class DependencyRegister
{
    public static void RegisterApiDependency(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
    }
}