using Common.Application;
using Proj.Application.Artists.Create;
using Proj.Application.Artists.Delete;
using Proj.Application.Artists.Edit;
using Proj.Query.Artists.Dtos;

namespace Proj.Presentation.Facade.Artists;

public interface IArtistFacade
{
    //Command
    Task<OperationResult> CreateArtist(CreateArtistCommand command);
    Task<OperationResult> EditArtist(EditArtistCommand command);
    Task<OperationResult> DeleteArtist(DeleteArtistCommand command);


    //Query
    Task<ArtistFilterResult> GetArtistsByFilter(ArtistFilterParams filterParams);
    Task<ArtistDto?> GetArtistsById(long artistId);
    Task<ArtistDto?> GetArtistsBySlug(string slug);
    Task<List<ArtistDto>> GetArtistList();
}