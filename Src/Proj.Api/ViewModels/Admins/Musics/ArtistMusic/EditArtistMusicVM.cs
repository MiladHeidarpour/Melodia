using Proj.Domain.MusicAgg.Enums;

namespace Proj.Api.ViewModels.Admins.Musics.ArtistMusic;

public class EditArtistMusicVM
{
    public long ArtistMusicId { get; set; }
    public long ArtistId { get; set; }
    public long MusicId { get; set; }
    public ArtistType ArtistType { get; set; }
}