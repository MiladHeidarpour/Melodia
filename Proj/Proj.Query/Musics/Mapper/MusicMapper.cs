﻿using Proj.Domain.MusicAgg;
using Proj.Domain.MusicAlbumAgg;
using Proj.Query.MusicAlbums.Dtos;
using Proj.Query.Musics.Dtos;

namespace Proj.Query.Musics.Mapper;

internal static class MusicMapper
{
    public static MusicDto? Map(this Music music)
    {
        if (music == null)
        {
            return null;
        }

        return new MusicDto()
        {
            Id = music.Id,
            CreationDate = music.CreationDate,
            TrackName = music.TrackName,
            AlbumId = music.AlbumId,
            TrackFile = music.TrackFile,
            TrackTime = music.TrackTime,
            ReleaseDate = music.ReleaseDate,
            Lyric = music.Lyric,
            Slug = music.Slug,
            SeoData = music.SeoData,
            ArtistMusics = music.ArtistMusics.Select(s=>new ArtistMusicDto()
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                MusicId = s.MusicId,
                ArtistId = s.ArtistId,
                ArtistType = s.ArtistType,
            }).ToList(),
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

    public static MusicFilterData MapListData(this Music music)
    {
        return new MusicFilterData()
        {
            Id = music.Id,
            CreationDate = music.CreationDate,
            Slug = music.Slug,
        };
    }
}