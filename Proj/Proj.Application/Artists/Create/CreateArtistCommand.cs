using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Artists.Create;

public record CreateArtistCommand(string ArtistName, IFormFile ArtistImg, long CategoryId, string? AboutArtist, string Slug, SeoData SeoData) : IBaseCommand;