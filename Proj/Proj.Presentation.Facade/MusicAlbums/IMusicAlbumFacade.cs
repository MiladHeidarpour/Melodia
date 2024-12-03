using Common.Application;
using Proj.Application.MusicAlbums.Create;
using Proj.Application.MusicAlbums.Delete;
using Proj.Application.MusicAlbums.Edit;
using Proj.Query.MusicAlbums.Dtos;

namespace Proj.Presentation.Facade.MusicAlbums;

public interface IMusicAlbumFacade
{
    //Command
    Task<OperationResult> CreateMusicAlbum(CreateMusicAlbumCommand command);
    Task<OperationResult> EditMusicAlbum(EditMusicAlbumCommand command);
    Task<OperationResult> DeleteMusicAlbum(DeleteMusicAlbumCommand command);


    //Query
    Task<MusicAlbumDto?> GetMusicAlbumById(long albumId);
    Task<MusicAlbumDto?> GetMusicAlbumBySlug(string slug);
    Task<List<MusicAlbumDto>> GetMusicAlbumList();
}