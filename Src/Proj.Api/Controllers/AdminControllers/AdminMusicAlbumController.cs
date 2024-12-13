using Common.AspNetCore;
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

    [HttpGet("{albumId}")]
    public async Task<ApiResult<MusicAlbumDto?>> GetMusicAlbumById(long albumId)
    {
        var album = await _facade.GetMusicAlbumById(albumId);
        return QueryResult(album);
    }

    [HttpGet("Slug/{slug}")]
    public async Task<ApiResult<MusicAlbumDto?>> GetMusicAlbumBySlug(string slug)
    {
        var album = await _facade.GetMusicAlbumBySlug(slug);
        return QueryResult(album);
    }

    [HttpGet("List")]
    public async Task<ApiResult<List<MusicAlbumDto>>> GetMusicAlbumList()
    {
        var albums = await _facade.GetMusicAlbumList();
        return QueryResult(albums);
    }

    #endregion

    #region Command

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

    [HttpDelete("{albumId}")]
    public async Task<ApiResult> DeleteMusicAlbum(long albumId)
    {
        var result = await _facade.DeleteMusicAlbum(new DeleteMusicAlbumCommand(albumId));
        return CommandResult(result);
    }
    #endregion
}