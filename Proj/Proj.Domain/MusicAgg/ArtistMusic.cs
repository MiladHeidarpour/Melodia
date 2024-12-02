using Common.Domain.Base;
using Proj.Domain.MusicAgg.Enums;

namespace Proj.Domain.MusicAgg;

public class ArtistMusic : BaseEntity
{
    public long ArtistId { get; set; }
    public long MusicId { get; set; }
    public ArtistType ArtistType { get; set; }

    public ArtistMusic(long artistId, long musicId, ArtistType artistType)
    {
        ArtistId = artistId;
        MusicId = musicId;
        ArtistType = artistType;
    }
}