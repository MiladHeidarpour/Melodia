using Common.Application;
using Proj.Application.Musics.AddArtistMusic;
using Proj.Application.Musics.AddListArtistMusics;
using Proj.Application.Musics.Create;
using Proj.Application.Musics.Delete;
using Proj.Application.Musics.DeleteArtistMusic;
using Proj.Application.Musics.Edit;
using Proj.Application.Musics.EditArtistMusic;
using Proj.Query.Musics.Dtos;

namespace Proj.Presentation.Facade.Musics;

public interface IMusicFacade
{
    //Command
    Task<OperationResult> CreateMusic(CreateMusicCommand command);
    Task<OperationResult> EditMusic(EditMusicCommand command);
    Task<OperationResult> DeleteMusic(DeleteMusicCommand command);

    //ArtistMusic Command
    Task<OperationResult> AddArtistMusic(AddArtistMusicCommand command);
    Task<OperationResult> AddArtistMusicList(AddListArtistMusicsCommand command);
    Task<OperationResult> EditArtistMusic(EditArtistMusicCommand command);
    Task<OperationResult> DeleteArtistMusic(DeleteArtistMusicCommand command);



    //Query
    Task<MusicFilterResult> GetMusicsByFilter(MusicFilterParams filterParams);
    Task<MusicDto?> GetMusicById(long musicId);
    Task<MusicDto?> GetMusicBySlug(string slug);
    Task<List<MusicDto>> GetMusicList();
    Task<List<MusicDto>> NewReleaseSongs();

    //ArtistMusic Query
    Task<ArtistMusicDto> GetArtistMusicById(long artistMusicId);
    Task<List<ArtistMusicDto>> GetArtistMusicList(long musicId);
}