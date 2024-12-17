using System.ComponentModel.DataAnnotations;

namespace Proj.Api.ViewModels.Admins.Musics;

public class EditMusicVM
{
    [Required]
    public long MusicId { get; set; }

    [Display(Name = "نام موزیک")]
    [Required(ErrorMessage = "نام موزیک را وارد کنید")]
    [MaxLength(250, ErrorMessage = "نام موزیک نباید بیشتر از 250 کارکتر باشد")]
    public string TrackName { get; set; }

    [Display(Name = "آلبوم")]
    [Required(ErrorMessage = "آلبوم را وارد کنید")]
    public long AlbumId { get; set; }

    [Display(Name = "فایل موزیک")]
    public IFormFile? TrackFile { get; set; }

    [Display(Name = "مدت زمان")]
    [Required(ErrorMessage = "مدت زمان را وارد کنید")]
    public string TrackTime { get; set; }

    [Display(Name = "تاریخ انتشار")]
    [Required(ErrorMessage = "تاریخ انتشار را وارد کنید")]
    public DateTime ReleaseDate { get; set; }

    [Display(Name = "متن")]
    public string? Lyric { get; set; }

    [Display(Name = "اسلاگ")]
    [Required(ErrorMessage = "اسلاگ را وارد کنید")]
    public string Slug { get; set; }

    [Display(Name = "SEO")]
    public SeoDataViewModel SeoData { get; set; }
}