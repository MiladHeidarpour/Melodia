using Common.Application;
using MediatR;
using Proj.Application.Musics.Create;
using Proj.Application.Musics.Delete;
using Proj.Application.Musics.Edit;
using Proj.Query.Musics.Dtos;
using Proj.Query.Musics.GetByFilter;
using Proj.Query.Musics.GetById;
using Proj.Query.Musics.GetBySlug;
using Proj.Query.Musics.GetList;

namespace Proj.Presentation.Facade.Musics;

internal class MusicFacade : IMusicFacade
{
    private readonly IMediator _mediator;

    public MusicFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateMusic(CreateMusicCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteMusic(DeleteMusicCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditMusic(EditMusicCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<MusicDto?> GetMusicById(long musicId)
    {
        return await _mediator.Send(new GetMusicByIdQuery(musicId));
    }

    public async Task<MusicDto?> GetMusicBySlug(string slug)
    {
        return await _mediator.Send(new GetMusicBySlugQuery(slug));
    }

    public async Task<List<MusicDto>> GetMusicList()
    {
        return await _mediator.Send(new GetMusicListQuery());
    }

    public async Task<MusicFilterResult> GetMusicsByFilter(MusicFilterParams filterParams)
    {
        return await _mediator.Send(new GetMusicByFilterQuery(filterParams));
    }
}