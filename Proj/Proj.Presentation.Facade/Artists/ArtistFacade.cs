using Common.Application;
using MediatR;
using Proj.Application.Artists.Create;
using Proj.Application.Artists.Delete;
using Proj.Application.Artists.Edit;
using Proj.Query.Artists.Dtos;
using Proj.Query.Artists.GetByFilter;
using Proj.Query.Artists.GetById;
using Proj.Query.Artists.GetBySlug;
using Proj.Query.Artists.GetList;

namespace Proj.Presentation.Facade.Artists;

internal class ArtistFacade : IArtistFacade
{
    private readonly IMediator _mediator;

    public ArtistFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateArtist(CreateArtistCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditArtist(EditArtistCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<OperationResult> DeleteArtist(DeleteArtistCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ArtistFilterResult> GetArtistsByFilter(ArtistFilterParams filterParams)
    {
        return await _mediator.Send(new GetArtistByFilterQuery(filterParams));
    }

    public async Task<ArtistDto?> GetArtistsById(long artistId)
    {
        return await _mediator.Send(new GetArtistByIdQuery(artistId));
    }

    public async Task<ArtistDto?> GetArtistsBySlug(string slug)
    {
        return await _mediator.Send(new GetArtistBySlugQuery(slug));
    }

    public async Task<List<ArtistDto>> GetArtistList()
    {
        return await _mediator.Send(new GetArtistListQuery());
    }
}