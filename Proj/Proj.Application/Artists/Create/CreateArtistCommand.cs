using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Artists.Create;

public class CreateArtistCommand : IBaseCommand
{
    public string ArtistName { get; set; }
    public IFormFile ArtistImg { get; set; }
    public long CategoryId { get; set; }
    public string? AboutArtist { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}