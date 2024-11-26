using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Artists.Edit;

public record EditArtistCommand(long ArtistId, string ArtistName, IFormFile? ArtistImg, string? AboutArtist, long CategoryId, string Slug, SeoData SeoData) : IBaseCommand;