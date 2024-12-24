using Common.AspNetCore._Utils;
using Microsoft.AspNetCore.Mvc;
using Proj.Api.Infrastructure;
using Proj.Api.ViewModels.Admins.Musics;
using Proj.Api.ViewModels.Admins.Musics.ArtistMusic;
using Proj.Application.Musics.AddListArtistMusics;
using Proj.Application.Musics.Create;
using Proj.Application.Musics.Delete;
using Proj.Application.Musics.DeleteArtistMusic;
using Proj.Application.Musics.Edit;
using Proj.Application.Musics.EditArtistMusic;
using Proj.Presentation.Facade.Musics;
using Proj.Query.Musics.Dtos;


namespace Proj.Api.Controllers.AdminControllers;

public class AdminMusicController : AdminApiController
{
    private readonly IMusicFacade _facade;

    public AdminMusicController(IMusicFacade facade)
    {
        _facade = facade;
    }

    #region Query

    /// <summary>
    /// جستوجوی موزیک بر اساس فیلتر
    /// </summary>
    /// <param name="filterParams">مقادیر جستوجو</param>
    /// <returns>موزیک های فیلتر شده</returns>
    [HttpGet("Filter")]
    public async Task<ApiResult<MusicFilterResult>> GetMusicByFilter(MusicFilterParams filterParams)
    {
        var musics = await _facade.GetMusicsByFilter(filterParams);
        return QueryResult(musics);
    }



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


    //ArtistMusic

    /// <summary>
    /// جستوجوی آرتیست های موزیک بر اساس شناسه آرتیست
    /// </summary>
    /// <param name="artistMusicId">شناسه آرتیست</param>
    /// <returns>آرتیست های موزیک</returns>
    [HttpGet("{artistMusicId}")]
    public async Task<ApiResult<ArtistMusicDto>> GetArtistMusic(long artistMusicId)
    {
        var result = await _facade.GetArtistMusicById(artistMusicId);
        return QueryResult(result);
    }



    /// <summary>
    /// جستوجوی آرتیست های موزیک بر اساس شناسه موزیک
    /// </summary>
    /// <param name="musicId">شناسه موزیک</param>
    /// <returns>آرتیست های موزیک</returns>
    [HttpGet("ArtistMusicList/{musicId}")]
    public async Task<ApiResult<List<ArtistMusicDto>>> GetArtistMusicList(long musicId)
    {
        var result = await _facade.GetArtistMusicList(musicId);
        return QueryResult(result);
    }
    #endregion

    #region Command

    /// <summary>
    /// ثبت موزیک
    /// </summary>
    /// <param name="command">اطلاعات موزیک</param>
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
            ArtistMusic = MapperProfile.Map(command.ArtistMusic),
        });
        return CommandResult(result);
    }



    /// <summary>
    /// ویرایش موزیک
    /// </summary>
    /// <param name="command">اطلاعات موزیک</param>
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



    /// <summary>
    /// حذف موزیک
    /// </summary>
    /// <param name="musicId">شناسه موزیک</param>
    [HttpDelete("{musicId}")]
    public async Task<ApiResult> DeleteMusic(long musicId)
    {
        var result = await _facade.DeleteMusic(new DeleteMusicCommand(musicId));
        return CommandResult(result);
    }


    //ArtistMusic


    //for adding one artistMusic

    //[HttpPost("AddArtistMusic")]
    //public async Task<ApiResult> AddArtistMusic(AddArtistMusicVM command)
    //{
    //    var result = await _facade.AddArtistMusic(new AddArtistMusicCommand()
    //    {
    //        ArtistId = command.ArtistId,
    //        MusicId = command.MusicId,
    //        ArtistType = command.ArtistType,
    //    });
    //    return CommandResult(result);
    //}


    /// <summary>
    /// افزودن آرتیست برای موزیک
    /// </summary>
    /// <param name="command">اطلاعات</param>
    [HttpPost("AddListArtistMusic")]
    public async Task<ApiResult> AddArtistMusic(List<AddArtistMusicVM> command)
    {
        var result = await _facade.AddArtistMusicList(new AddListArtistMusicsCommand()
        {
            Artists = MapperProfile.Map(command),
        });
        return CommandResult(result);
    }



    /// <summary>
    /// ویرایش آرتیست برای موزیک
    /// </summary>
    /// <param name="command">اطلاعات</param>
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



    /// <summary>
    /// حذف آرتیست برای موزیک
    /// </summary>
    /// <param name="command">اطلاعات</param>
    [HttpDelete("DeleteArtistMusic")]
    public async Task<ApiResult> DeleteArtistMusic(DeleteArtistMusicCommand command)
    {
        var result = await _facade.DeleteArtistMusic(command);
        return CommandResult(result);
    }
    #endregion
}