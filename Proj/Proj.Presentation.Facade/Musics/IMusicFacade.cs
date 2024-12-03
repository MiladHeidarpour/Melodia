using Common.Application;
using Proj.Application.Musics.Create;
using Proj.Application.Musics.Delete;
using Proj.Application.Musics.Edit;
using Proj.Query.Musics.Dtos;

namespace Proj.Presentation.Facade.Musics;

public interface IMusicFacade
{
    //Command
    Task<OperationResult> CreateMusic(CreateMusicCommand command);
    Task<OperationResult> EditMusic(EditMusicCommand command);
    Task<OperationResult> DeleteMusic(DeleteMusicCommand command);


    //Query
    Task<MusicFilterResult> GetMusicsByFilter(MusicFilterParams filterParams);
    Task<MusicDto?> GetMusicById(long musicId);
    Task<MusicDto?> GetMusicBySlug(string slug);
    Task<List<MusicDto>> GetMusicList();
}