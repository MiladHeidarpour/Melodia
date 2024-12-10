﻿using Shop.Api.ViewModels.Products;

namespace Proj.Api.ViewModels.Admins.Artists;

public class CreateArtistVM
{
    public string ArtistName { get; set; }
    public IFormFile ArtistImg { get; set; }
    public long CategoryId { get; set; }
    public string? AboutArtist { get; set; }
    public string Slug { get; set; }
    public SeoDataViewModel SeoData { get; set; }
}