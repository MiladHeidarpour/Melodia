using Common.Application;
using Proj.Domain.MusicAgg.Enums;

namespace Proj.Application.Musics.EditArtistMusic;

public class EditArtistMusicCommand : IBaseCommand
{
    public long ArtistMusicId { get; set; }
    public long ArtistId { get; set; }
    public long MusicId { get; set; }
    public ArtistType ArtistType { get; set; }
}