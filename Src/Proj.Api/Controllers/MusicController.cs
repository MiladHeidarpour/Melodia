using Common.AspNetCore._Utils;
using Microsoft.AspNetCore.Mvc;
using Proj.Presentation.Facade.Musics;
using Proj.Query.Musics.Dtos;

namespace Proj.Api.Controllers;
public class MusicController : ApiController
{
    private readonly IMusicFacade _facade;

    public MusicController(IMusicFacade facade)
    {
        _facade = facade;
    }

    #region Query

    /// <summary>
    /// جستوجوی موزیک بر اساس شناسه موزیک
    /// </summary>
    /// <param name="musicId">شناسه موزیک</param>
    /// <returns>موزیک</returns>
    [HttpGet("{musicId:long}")]
    public async Task<ApiResult<MusicDto?>> GetMusicById(long musicId)
    {
        var music = await _facade.GetMusicById(musicId);
        return QueryResult(music);
    }


    /// <summary>
    /// جستوجوی موزیک بر اساس شناسه اسلاگ
    /// </summary>
    /// <param name="slug">شناسه اسلاگ</param>
    /// <returns>موزیک</returns>
    [HttpGet("{slug}")]
    public async Task<ApiResult<MusicDto?>> GetMusicBySlug(string slug)
    {
        var music = await _facade.GetMusicBySlug(slug);
        return QueryResult(music);
    }



    /// <summary>
    /// لیستی از تمام موزیک ها
    /// </summary>
    /// <returns>لیستی از تمام موزیک ها</returns>
    [HttpGet("List")]
    public async Task<ApiResult<List<MusicDto>>> GetMusicList()
    {
        var musics = await _facade.GetMusicList();
        return QueryResult(musics);
    }



    /// <summary>
    /// New Release Songs
    /// </summary>
    /// <returns>لیستی از تمام موزیک های جدید</returns>
    [HttpGet("NewReleaseSongs")]
    public async Task<ApiResult<List<MusicDto>>> NewReleaseSongs()
    {
        var musics = await _facade.NewReleaseSongs();
        return QueryResult(musics);
    }
    #endregion

}
