using Common.AspNetCore._Utils;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Proj.Api.ViewModels.Admins.Artists;
using Proj.Application.Artists.Create;
using Proj.Application.Artists.Delete;
using Proj.Application.Artists.Edit;
using Proj.Presentation.Facade.Artists;
using Proj.Query.Artists.Dtos;

namespace Proj.Api.Controllers.AdminControllers;

public class AdminArtistController : AdminApiController
{
    private readonly IArtistFacade _facade;

    public AdminArtistController(IArtistFacade facade)
    {
        _facade = facade;
    }

    #region Query

    /// <summary>
    /// جستوجوی آرتیست بر اساس فیلتر
    /// </summary>
    /// <param name="filterParams">مقادیر جستوجو</param>
    /// <returns>آرتیست های فیلتر شده</returns>
    [HttpGet("Filter")]
    public async Task<ApiResult<ArtistFilterResult>> GetArtistByFilter([FromQuery] ArtistFilterParams filterParams)
    {
        var artists = await _facade.GetArtistsByFilter(filterParams);
        return QueryResult(artists);
    }



    /// <summary>
    /// جستوجوی آرتیست بر اساس شناسه آرتیست
    /// </summary>
    /// <param name="artistId">شناسه آرتیست</param>
    /// <returns>آرتیست</returns>
    [HttpGet("{artistId}")]
    public async Task<ApiResult<ArtistDto?>> GetArtistById(long artistId)
    {
        var artist = await _facade.GetArtistsById(artistId);
        return QueryResult(artist);
    }



    /// <summary>
    /// جستوجوی آرتیست بر اساس شناسه اسلاگ
    /// </summary>
    /// <param name="slug">شناسه اسلاگ</param>
    /// <returns>آرتیست</returns>
    [HttpGet("BySlug/{slug}")]
    public async Task<ApiResult<ArtistDto?>> GetArtistBySlug(string slug)
    {
        var artist = await _facade.GetArtistsBySlug(slug);
        return QueryResult(artist);
    }



    /// <summary>
    /// لیستی از تمام آرتیست ها
    /// </summary>
    /// <returns>لیستی از تمام آرتیست ها</returns>
    [HttpGet("Lists")]
    public async Task<ApiResult<List<ArtistDto>>> GetArtistList()
    {
        var artists = await _facade.GetArtistList();
        return QueryResult(artists);
    }
    #endregion

    #region Command


    /// <summary>
    /// ثبت آرتیست
    /// </summary>
    /// <param name="command">اطلاعات آرتیست</param>
    [HttpPost]
    public async Task<ApiResult> CreateArtist([FromForm] CreateArtistVM command)
    {
        var result = await _facade.CreateArtist(new CreateArtistCommand()
        {
            ArtistName = command.ArtistName,
            ArtistImg = command.ArtistImg,
            CategoryId = command.CategoryId,
            AboutArtist = command.AboutArtist,
            Slug = command.Slug,
            SeoData = command.SeoData.Map(),
        });
        return CommandResult(result);
    }



    /// <summary>
    /// ویرایش آرتیست
    /// </summary>
    /// <param name="command">اطلاعات آرتیست</param>
    [HttpPut]
    public async Task<ApiResult> EditArtist([FromForm] EditArtistVM command)
    {
        var result = await _facade.EditArtist(new EditArtistCommand()
        {
            ArtistId = command.ArtistId,
            ArtistName = command.ArtistName,
            ArtistImg = command.ArtistImg,
            CategoryId = command.CategoryId,
            AboutArtist = command.AboutArtist,
            Slug = command.Slug,
            SeoData = command.SeoData.Map(),
        });
        return CommandResult(result);
    }



    /// <summary>
    /// حذف آرتیست
    /// </summary>
    /// <param name="artistId">شناسه آرتیست</param>
    [HttpDelete("{artistId}")]
    public async Task<ApiResult> DeleteArtist(long artistId)
    {
        var result = await _facade.DeleteArtist(new DeleteArtistCommand(artistId));
        return CommandResult(result);
    }

    #endregion
}