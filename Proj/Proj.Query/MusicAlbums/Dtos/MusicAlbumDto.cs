using Common.Domain.ValueObjects;
using Common.Query;
using Proj.Domain.MusicAlbumAgg.Enums;

namespace Proj.Query.MusicAlbums.Dtos;

public class MusicAlbumDto : BaseDto
{
    public string AlbumName { get; set; }
    public string CoverImg { get; set; }
    public long CategoryId { get; set; }
    public AlbumType AlbumType { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}