using Common.Query;
using Proj.Domain.MusicAgg.Enums;

namespace Proj.Query.Musics.Dtos;

public class ArtistMusicDto : BaseDto
{
    public long ArtistId { get; set; }
    public long MusicId { get; set; }
    public ArtistType ArtistType { get; set; }
}