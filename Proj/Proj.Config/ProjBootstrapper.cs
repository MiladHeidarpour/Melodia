﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Proj.Application.Artists.Create;
using Proj.Application.Artists.Services;
using Proj.Application.Categories.Services;
using Proj.Application.MusicAlbums.Services;
using Proj.Application.Musics.Services;
using Proj.Application.Roles.Services;
using Proj.Application.Users.Services;
using Proj.Domain.ArtistAgg.Services;
using Proj.Domain.CategoryAgg.Services;
using Proj.Domain.MusicAgg.Services;
using Proj.Domain.MusicAlbumAgg.Services;
using Proj.Domain.RoleAgg.Service;
using Proj.Domain.UserAgg.Services;
using Proj.Infrastructure;
using Proj.Presentation.Facade;
using Proj.Query.Artists.GetById;

namespace Proj.Config;

public static class ProjBootstrapper
{
    public static void RegisterProjDependency(this IServiceCollection services, string connectionString)
    {
        InfrastructureBootstrapper.Init(services, connectionString);

        services.AddMediatR(typeof(CreateArtistCommand).Assembly);
        services.AddMediatR(typeof(GetArtistByIdQuery).Assembly);

        services.AddTransient<IArtistDomainService, ArtistDomainService>();
        services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        services.AddTransient<IMusicAlbumDomainService, MusicAlbumDomainService>();
        services.AddTransient<IMusicDomainService, MusicDomainService>();
        services.AddTransient<IUserDomainService, UserDomainService>();
        services.AddTransient<IRoleDomainService, RoleDomianService>();

        services.AddValidatorsFromAssembly(typeof(CreateArtistCommandValidator).Assembly);
        services.InitFacadeDependency();
    }
}