using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAlbumAgg;
using Proj.Query.MusicAlbums.Dtos;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.MusicAlbums.Mapper;

internal static class MusicAlbumMapper
{
    public static MusicAlbumDto? Map(this MusicAlbum musicAlbum)
    {
        if (musicAlbum == null)
        {
            return null;
        }

        return new MusicAlbumDto()
        {
            Id = musicAlbum.Id,
            CreationDate = musicAlbum.CreationDate,
            AlbumName = musicAlbum.AlbumName,
            AlbumType = musicAlbum.AlbumType,
            CategoryId = musicAlbum.CategoryId,
            CoverImg = musicAlbum.CoverImg,
            Slug = musicAlbum.Slug,
            SeoData = musicAlbum.SeoData,
        };
    }

    public static List<MusicAlbumDto>? Map(this List<MusicAlbum> musicAlbums)
    {
        var model = new List<MusicAlbumDto>();
        musicAlbums.ForEach(musicAlbum =>
        {
            model.Add(new MusicAlbumDto()
            {
                Id = musicAlbum.Id,
                CreationDate = musicAlbum.CreationDate,
                AlbumName = musicAlbum.AlbumName,
                AlbumType = musicAlbum.AlbumType,
                CategoryId = musicAlbum.CategoryId,
                CoverImg = musicAlbum.CoverImg,
                Slug = musicAlbum.Slug,
                SeoData = musicAlbum.SeoData,
            });
        });
        return model;
    }

    public static MusicAlbumFilterData MapListData(this MusicAlbum musicAlbum)
    {
        return new MusicAlbumFilterData()
        {
            Id = musicAlbum.Id,
            CreationDate = musicAlbum.CreationDate,
            Slug = musicAlbum.Slug,
        };
    }
}