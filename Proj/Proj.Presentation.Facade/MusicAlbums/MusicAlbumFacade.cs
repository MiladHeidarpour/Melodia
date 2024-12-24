using Common.Application;
using MediatR;
using Proj.Application.MusicAlbums.Create;
using Proj.Application.MusicAlbums.Delete;
using Proj.Application.MusicAlbums.Edit;
using Proj.Query.MusicAlbums.Dtos;
using Proj.Query.MusicAlbums.GetByFilter;
using Proj.Query.MusicAlbums.GetById;
using Proj.Query.MusicAlbums.GetBySlug;
using Proj.Query.MusicAlbums.GetList;

namespace Proj.Presentation.Facade.MusicAlbums;

internal class MusicAlbumFacade : IMusicAlbumFacade
{
    private readonly IMediator _mediator;

    public MusicAlbumFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateMusicAlbum(CreateMusicAlbumCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditMusicAlbum(EditMusicAlbumCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteMusicAlbum(DeleteMusicAlbumCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<MusicAlbumFilterResult> GetMusicAlbumByFilter(MusicAlbumFilterParams filterParams)
    {
        return await _mediator.Send(new GetMusicAlbumByFilterQuery(filterParams));
    }

    public async Task<MusicAlbumDto?> GetMusicAlbumById(long albumId)
    {
        return await _mediator.Send(new GetMusicAlbumByIdQuery(albumId));
    }

    public async Task<MusicAlbumDto?> GetMusicAlbumBySlug(string slug)
    {
        return await _mediator.Send(new GetMusicAlbumBySlugQuery(slug));
    }

    public async Task<List<MusicAlbumDto>> GetMusicAlbumList()
    {
        return await _mediator.Send(new GetMusicAlbumListQuery());
    }
}