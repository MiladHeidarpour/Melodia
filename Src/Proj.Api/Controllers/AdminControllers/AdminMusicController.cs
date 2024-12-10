using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Proj.Application.Musics.Create;
using Proj.Application.Musics.Delete;
using Proj.Application.Musics.Edit;
using Proj.Presentation.Facade.Musics;
using Proj.Query.Musics.Dtos;
using Shop.Api.ViewModels.Products.Musics;


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
    #endregion

    #region Command

    [HttpPost]
    public async Task<ApiResult> CreateMusic([FromForm] CreateMusicVM command)
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
        });
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditMusic([FromForm] EditMusicVM command)
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
    #endregion
}