using Proj.Domain.MusicAgg.Enums;

namespace Proj.Api.ViewModels.Admins.Musics.ArtistMusic;

public class AddArtistMusicVM
{
    public long ArtistId { get; set; }
    public long MusicId { get; set; }
    public ArtistType ArtistType { get; set; }
}