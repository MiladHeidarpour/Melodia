﻿using Proj.Domain.MusicAlbumAgg.Enums;

namespace Shop.Api.ViewModels.Products.MusicAlbums;

public class CreateMusicAlbumVM
{
    public string AlbumName { get; set; }
    public IFormFile CoverImg { get; set; }
    public long CategoryId { get; set; }
    public AlbumType AlbumType { get; set; }
    public string Slug { get; set; }
    public SeoDataViewModel SeoData { get; set; }
}