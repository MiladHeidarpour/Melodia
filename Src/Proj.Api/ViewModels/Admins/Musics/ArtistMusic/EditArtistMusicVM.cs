using System.ComponentModel.DataAnnotations;
using Proj.Domain.MusicAgg.Enums;

namespace Proj.Api.ViewModels.Admins.Musics.ArtistMusic;

public class EditArtistMusicVM
{
    [Required]
    public long ArtistMusicId { get; set; }

    [Display(Name = "آرتیست")]
    [Required(ErrorMessage = "آرتیست را وارد کنید")]
    public long ArtistId { get; set; }

    [Display(Name = "موزیک")]
    [Required(ErrorMessage = "موزیک را وارد کنید")]
    public long MusicId { get; set; }

    [Display(Name = "نوع آرتیست")]
    [Required(ErrorMessage = "نوع آرتیست را وارد کنید")]
    public ArtistType ArtistType { get; set; }
}