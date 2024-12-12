using Proj.Domain.MusicAgg.Enums;

namespace Shop.Api.ViewModels.Products.Musics.ArtistMusic;

public class EditArtistMusicVM
{
    public long ArtistMusicId { get; set; }
    public long ArtistId { get; set; }
    public long MusicId { get; set; }
    public ArtistType ArtistType { get; set; }
}