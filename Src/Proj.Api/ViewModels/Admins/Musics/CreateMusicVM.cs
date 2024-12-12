using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Products.Musics.ArtistMusic;

namespace Shop.Api.ViewModels.Products.Musics;

public class CreateMusicVM
{
    public string TrackName { get; set; }
    public long AlbumId { get; set; }
    public IFormFile TrackFile { get; set; }
    public string TrackTime { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Lyric { get; set; }
    public string Slug { get; set; }
    public SeoDataViewModel SeoData { get; set; }
    public List<AddArtistMusicVM> ArtistMusics { get; set; }=new List<AddArtistMusicVM>();
}