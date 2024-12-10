using Common.AspNetCore;
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

    [HttpGet("Filter")]
    public async Task<ApiResult<ArtistFilterResult>> GetArtistByFilter([FromQuery] ArtistFilterParams filterParams)
    {
        var artists = await _facade.GetArtistsByFilter(filterParams);
        return QueryResult(artists);
    }

    [HttpGet("{artistId}")]
    public async Task<ApiResult<ArtistDto?>> GetArtistById(long artistId)
    {
        var artist = await _facade.GetArtistsById(artistId);
        return QueryResult(artist);
    }

    [HttpGet("BySlug/{slug}")]
    public async Task<ApiResult<ArtistDto?>> GetArtistBySlug(string slug)
    {
        var artist = await _facade.GetArtistsBySlug(slug);
        return QueryResult(artist);
    }

    [HttpGet("Lists")]
    public async Task<ApiResult<List<ArtistDto>>> GetArtistList()
    {
        var artists = await _facade.GetArtistList();
        return QueryResult(artists);
    }
    #endregion


    #region Command

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

    [HttpDelete("{artistId}")]
    public async Task<ApiResult> DeleteArtist(long artistId)
    {
        var result = await _facade.DeleteArtist(new DeleteArtistCommand(artistId));
        return CommandResult(result);
    }

    #endregion
}