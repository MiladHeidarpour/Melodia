using Common.AspNetCore._Utils;
using Microsoft.AspNetCore.Mvc;
using Proj.Api.ViewModels.Admins.MusicAlbums;
using Proj.Application.MusicAlbums.Create;
using Proj.Application.MusicAlbums.Delete;
using Proj.Application.MusicAlbums.Edit;
using Proj.Presentation.Facade.MusicAlbums;
using Proj.Query.MusicAlbums.Dtos;

namespace Proj.Api.Controllers.AdminControllers;

public class AdminMusicAlbumController : AdminApiController
{
    private readonly IMusicAlbumFacade _facade;

    public AdminMusicAlbumController(IMusicAlbumFacade facade)
    {
        _facade = facade;
    }

    #region Query

    /// <summary>
    /// جستوجوی آلبوم بر اساس فیلتر
    /// </summary>
    /// <param name="filterParams">مقادیر جستوجو</param>
    /// <returns>آلبوم های فیلتر شده</returns>
    [HttpGet("Filter")]
    public async Task<ApiResult<MusicAlbumFilterResult>> GetMusicAlbumByFilter(MusicAlbumFilterParams filterParams)
    {
        var musics = await _facade.GetMusicAlbumByFilter(filterParams);
        return QueryResult(musics);
    }



    /// <summary>
    /// جستوجوی آلبوم بر اساس شناسه آلبوم
    /// </summary>
    /// <param name="albumId">شناسه آلبوم</param>
    /// <returns>آلبوم</returns>
    [HttpGet("{albumId}")]
    public async Task<ApiResult<MusicAlbumDto?>> GetMusicAlbumById(long albumId)
    {
        var album = await _facade.GetMusicAlbumById(albumId);
        return QueryResult(album);
    }



    /// <summary>
    /// جستوجوی آلبوم بر اساس شناسه اسلاگ
    /// </summary>
    /// <param name="slug">شناسه اسلاگ</param>
    /// <returns>آلبوم</returns>
    [HttpGet("Slug/{slug}")]
    public async Task<ApiResult<MusicAlbumDto?>> GetMusicAlbumBySlug(string slug)
    {
        var album = await _facade.GetMusicAlbumBySlug(slug);
        return QueryResult(album);
    }



    /// <summary>
    /// لیستی از تمام آلبوم ها
    /// </summary>
    /// <returns>لیستی از تمام آلبوم ها</returns>
    [HttpGet("List")]
    public async Task<ApiResult<List<MusicAlbumDto>>> GetMusicAlbumList()
    {
        var albums = await _facade.GetMusicAlbumList();
        return QueryResult(albums);
    }

    #endregion

    #region Command

    /// <summary>
    /// ثبت آلبوم
    /// </summary>
    /// <param name="command">اطلاعات آلبوم</param>
    [HttpPost]
    public async Task<ApiResult> CreateMusicAlbum([FromForm] CreateMusicAlbumVM command)
    {
        var result = await _facade.CreateMusicAlbum(new CreateMusicAlbumCommand()
        {
            AlbumName = command.AlbumName,
            CoverImg = command.CoverImg,
            CategoryId = command.CategoryId,
            AlbumType = command.AlbumType,
            Slug = command.Slug,
            SeoData = command.SeoData.Map(),
        });
        return CommandResult(result);
    }



    /// <summary>
    /// ویرایش آلبوم
    /// </summary>
    /// <param name="command">اطلاعات آلبوم</param>
    [HttpPut]
    public async Task<ApiResult> EditMusicAlbum([FromForm] EditMusicAlbumVM command)
    {
        var result = await _facade.EditMusicAlbum(new EditMusicAlbumCommand()
        {
            AlbumId = command.AlbumId,
            AlbumName = command.AlbumName,
            CoverImg = command.CoverImg,
            CategoryId = command.CategoryId,
            AlbumType = command.AlbumType,
            Slug = command.Slug,
            SeoData = command.SeoData.Map(),
        });
        return CommandResult(result);
    }



    /// <summary>
    /// حذف آلبوم
    /// </summary>
    /// <param name="albumId">شناسه آلبوم</param>
    [HttpDelete("{albumId}")]
    public async Task<ApiResult> DeleteMusicAlbum(long albumId)
    {
        var result = await _facade.DeleteMusicAlbum(new DeleteMusicAlbumCommand(albumId));
        return CommandResult(result);
    }
    #endregion
}