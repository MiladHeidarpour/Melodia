using AutoMapper;
using Proj.Api.ViewModels.Admins.Musics;
using Proj.Api.ViewModels.Admins.Musics.ArtistMusic;
using Proj.Application.Musics.Create;
using Proj.Domain.MusicAgg;

namespace Proj.Api.Infrastructure;

public class MapperProfile : Profile
{
    public static List<ArtistMusic> Map(List<AddArtistMusicVM> artistMusicVms)
    {
        var model = new List<ArtistMusic>();
        artistMusicVms.ForEach(music =>
        {
            model.Add(new ArtistMusic(music.ArtistId, 0, music.ArtistType));
        });
        return model;
    }

    public static ArtistMusic Map(AddArtistMusicVM artistMusicVms)
    {
        return new ArtistMusic(artistMusicVms.ArtistId, 0, artistMusicVms.ArtistType);
    }
    public MapperProfile()
    {
        CreateMap<AddArtistMusicVM, ArtistMusic>().ReverseMap();
        CreateMap<CreateMusicVM, CreateMusicCommand>().ReverseMap();
    }
}
