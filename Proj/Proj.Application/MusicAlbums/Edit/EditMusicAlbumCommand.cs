﻿using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Proj.Domain.MusicAlbumAgg.Enums;

namespace Proj.Application.MusicAlbums.Edit;

public class EditMusicAlbumCommand : IBaseCommand
{
    public long AlbumId { get; set; }
    public string AlbumName { get; set; }
    public IFormFile? CoverImg { get; set; }
    public long CategoryId { get; set; }
    public AlbumType AlbumType { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public EditMusicAlbumCommand(long albumId, string albumName, IFormFile? coverImg, AlbumType albumType, int numberOfSongs, string slug, SeoData seoData)
    {
        AlbumId = albumId;
        AlbumName = albumName;
        CoverImg = coverImg;
        AlbumType = albumType;
        Slug = slug;
        SeoData = seoData;
    }
}