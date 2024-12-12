using Common.Application;
using Proj.Domain.MusicAgg;

namespace Proj.Application.Musics.AddListArtistMusics;

public class AddListArtistMusicsCommand:IBaseCommand
{
    public List<ArtistMusic> Artists { get; set; }
}