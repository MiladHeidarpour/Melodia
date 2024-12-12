using AutoMapper;
using Proj.Application.Musics.Create;
using Proj.Domain.MusicAgg;
using Shop.Api.ViewModels.Products.Musics;
using Shop.Api.ViewModels.Products.Musics.ArtistMusic;

namespace Proj.Api.Infrastructure;

public class MapperProfile : Profile
{
    public static List<ArtistMusic> Map(List<AddArtistMusicVM> artistMusicVms)
    {
        var model = new List<ArtistMusic>();
        artistMusicVms.ForEach(music =>
        {
            model.Add(new ArtistMusic(music.ArtistId, music.MusicId, music.ArtistType));
        });
        return model;
    }

    public static ArtistMusic Map(AddArtistMusicVM artistMusicVms)
    {
        return new ArtistMusic(artistMusicVms.ArtistId, artistMusicVms.MusicId, artistMusicVms.ArtistType);
    }
    public MapperProfile()
    {
        CreateMap<AddArtistMusicVM, ArtistMusic>().ReverseMap();
        CreateMap<CreateMusicVM, CreateMusicCommand>().ReverseMap();
    }
}
