using Common.Application;
using Proj.Domain.MusicAgg.Enums;

namespace Proj.Application.Musics.AddArtistMusic;

public class AddArtistMusicCommand : IBaseCommand
{
    public long ArtistId { get; set; }
    public long MusicId { get; set; }
    public ArtistType ArtistType { get; set; }
}