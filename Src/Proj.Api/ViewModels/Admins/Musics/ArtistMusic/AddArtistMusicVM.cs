using Proj.Domain.MusicAgg.Enums;

namespace Shop.Api.ViewModels.Products.Musics.ArtistMusic;

public class AddArtistMusicVM
{
    public long ArtistId { get; set; }
    public long MusicId { get; set; }
    public ArtistType ArtistType { get; set; }
}