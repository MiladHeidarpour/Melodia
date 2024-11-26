using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Proj.Domain.MusicAgg;

namespace Proj.Application.MusicAlbums.Edit;

public class EditMusicAlbumCommand:IBaseCommand
{
    public long AlbumId { get;private set; }
    public string AlbumName { get; private set; }
    public IFormFile CoverImg { get; private set; }
    public TimeSpan AlbumTime { get; private set; }
    public int NumberOfSongs { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<Music> Musics { get; private set; }

    public EditMusicAlbumCommand(long albumId,string albumName, IFormFile coverImg, TimeSpan albumTime, int numberOfSongs, string slug, SeoData seoData, List<Music> musics)
    {
        AlbumId = albumId;
        AlbumName = albumName;
        CoverImg = coverImg;
        AlbumTime = albumTime;
        NumberOfSongs = numberOfSongs;
        Slug = slug;
        SeoData = seoData;
        Musics = musics;
    }
}