using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Artists.Edit;

public class EditArtistCommand : IBaseCommand
{
    public long ArtistId { get; set; }
    public string ArtistName { get; set; }
    public IFormFile? ArtistImg { get; set; }
    public long CategoryId { get; set; }
    public string? AboutArtist { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}