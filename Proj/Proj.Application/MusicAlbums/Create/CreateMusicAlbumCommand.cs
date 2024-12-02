using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Proj.Domain.MusicAlbumAgg.Enums;

namespace Proj.Application.MusicAlbums.Create;

public class CreateMusicAlbumCommand : IBaseCommand
{
    public string AlbumName { get; set; }
    public IFormFile CoverImg { get; set; }
    public long CategoryId { get; set; }
    public AlbumType AlbumType { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }

    public CreateMusicAlbumCommand(string albumName, IFormFile coverImg, long categoryId, AlbumType albumType, string slug, SeoData seoData)
    {
        AlbumName = albumName;
        CoverImg = coverImg;
        CategoryId = categoryId;
        AlbumType = albumType;
        Slug = slug;
        SeoData = seoData;
    }
}