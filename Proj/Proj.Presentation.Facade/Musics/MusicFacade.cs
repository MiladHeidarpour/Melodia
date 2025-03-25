using Common.Application;
using MediatR;
using Proj.Application.Musics.AddArtistMusic;
using Proj.Application.Musics.AddListArtistMusics;
using Proj.Application.Musics.Create;
using Proj.Application.Musics.Delete;
using Proj.Application.Musics.DeleteArtistMusic;
using Proj.Application.Musics.Edit;
using Proj.Application.Musics.EditArtistMusic;
using Proj.Domain.MusicAgg;
using Proj.Query.Musics.ArtistMusic.GetById;
using Proj.Query.Musics.ArtistMusic.GetList;
using Proj.Query.Musics.Dtos;
using Proj.Query.Musics.GetByFilter;
using Proj.Query.Musics.GetById;
using Proj.Query.Musics.GetBySlug;
using Proj.Query.Musics.GetList;
using Proj.Query.Musics.NewReleaseSongs;

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

    public async Task<OperationResult> AddArtistMusic(AddArtistMusicCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddArtistMusicList(AddListArtistMusicsCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditArtistMusic(EditArtistMusicCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteArtistMusic(DeleteArtistMusicCommand command)
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

    public async Task<List<MusicDto>> NewReleaseSongs()
    {
        return await _mediator.Send(new NewReleaseSongsQuery());
    }

    public async Task<ArtistMusicDto> GetArtistMusicById(long artistMusicId)
    {
        return await _mediator.Send(new GetArtistMusicByIdQuery(artistMusicId));
    }

    public async Task<List<ArtistMusicDto>> GetArtistMusicList(long musicId)
    {
        return await _mediator.Send(new GetArtistMusicListQuery(musicId));
    }

    public async Task<MusicFilterResult> GetMusicsByFilter(MusicFilterParams filterParams)
    {
        return await _mediator.Send(new GetMusicByFilterQuery(filterParams));
    }
}