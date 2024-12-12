using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proj.Api.Infrastructure;
using Proj.Application.Musics.AddArtistMusic;
using Proj.Application.Musics.Create;
using Proj.Application.Musics.Delete;
using Proj.Application.Musics.DeleteArtistMusic;
using Proj.Application.Musics.Edit;
using Proj.Application.Musics.EditArtistMusic;
using Proj.Presentation.Facade.Musics;
using Proj.Query.Musics.Dtos;
using Shop.Api.ViewModels.Products.Musics;
using Shop.Api.ViewModels.Products.Musics.ArtistMusic;


namespace Proj.Api.Controllers.AdminControllers;

public class AdminMusicController : AdminApiController
{
    private readonly IMusicFacade _facade;

    public AdminMusicController(IMusicFacade facade)
    {
        _facade = facade;
    }

    #region Query

    [HttpGet("Filter")]
    public async Task<ApiResult<MusicFilterResult>> GetMusicByFilter(MusicFilterParams filterParams)
    {
        var musics = await _facade.GetMusicsByFilter(filterParams);
        return QueryResult(musics);
    }

    [HttpGet("{musicId}")]
    public async Task<ApiResult<MusicDto?>> GetMusicById(long musicId)
    {
        var music = await _facade.GetMusicById(musicId);
        return QueryResult(music);
    }

    [HttpGet("{slug}")]
    public async Task<ApiResult<MusicDto?>> GetMusicBySlug(string slug)
    {
        var music = await _facade.GetMusicBySlug(slug);
        return QueryResult(music);
    }

    [HttpGet("List")]
    public async Task<ApiResult<List<MusicDto>>> GetMusicList()
    {
        var musics = await _facade.GetMusicList();
        return QueryResult(musics);
    }


    //ArtistMusic
    [HttpGet("{artistMusicId}")]
    public async Task<ApiResult<ArtistMusicDto>> GetArtistMusic(long artistMusicId)
    {
        var result = await _facade.GetArtistMusicById(artistMusicId);
        return QueryResult(result);
    }
    [HttpGet("ArtistMusicList/{musicId}")]
    public async Task<ApiResult<List<ArtistMusicDto>>> GetArtistMusicList(long musicId)
    {
        var result = await _facade.GetArtistMusicList(musicId);
        return QueryResult(result);
    }
    #endregion

    #region Command

    [HttpPost]
    public async Task<ApiResult> CreateMusic([FromBody] CreateMusicVM command)
    {
        var result = await _facade.CreateMusic(new CreateMusicCommand()
        {
            TrackName = command.TrackName,
            AlbumId = command.AlbumId,
            TrackFile = command.TrackFile,
            TrackTime = command.TrackTime,
            ReleaseDate = command.ReleaseDate,
            Lyric = command.Lyric,
            Slug = command.Slug,
            SeoData = command.SeoData.Map(),
            ArtistMusics = MapperProfile.Map(command.ArtistMusics),
        });
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditMusic([FromBody] EditMusicVM command)
    {
        var result = await _facade.EditMusic(new EditMusicCommand()
        {
            MusicId = command.MusicId,
            TrackName = command.TrackName,
            AlbumId = command.AlbumId,
            TrackFile = command.TrackFile,
            TrackTime = command.TrackTime,
            ReleaseDate = command.ReleaseDate,
            Lyric = command.Lyric,
            Slug = command.Slug,
            SeoData = command.SeoData.Map(),
        });
        return CommandResult(result);
    }

    [HttpDelete("{musicId}")]
    public async Task<ApiResult> DeleteMusic(long musicId)
    {
        var result = await _facade.DeleteMusic(new DeleteMusicCommand(musicId));
        return CommandResult(result);
    }

    //ArtistMusic
    [HttpPost("AddArtistMusic")]
    public async Task<ApiResult> AddArtistMusic(AddArtistMusicVM command)
    {
        var result = await _facade.AddArtistMusic(new AddArtistMusicCommand()
        {
            ArtistId = command.ArtistId,
            MusicId = command.MusicId,
            ArtistType = command.ArtistType,
        });
        return CommandResult(result);
    }

    [HttpPut("EditArtistMusic")]
    public async Task<ApiResult> EditArtistMusic(EditArtistMusicVM command)
    {
        var result = await _facade.EditArtistMusic(new EditArtistMusicCommand()
        {
            ArtistMusicId = command.ArtistMusicId,
            ArtistId = command.ArtistId,
            MusicId = command.MusicId,
            ArtistType = command.ArtistType,
        });
        return CommandResult(result);
    }

    [HttpDelete("DeleteArtistMusic")]
    public async Task<ApiResult> DeleteArtistMusic(DeleteArtistMusicCommand command)
    {
        var result = await _facade.DeleteArtistMusic(command);
        return CommandResult(result);
    }
    #endregion
}