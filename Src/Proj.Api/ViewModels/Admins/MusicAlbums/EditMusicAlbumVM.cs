using System.ComponentModel.DataAnnotations;
using Proj.Domain.MusicAlbumAgg.Enums;

namespace Proj.Api.ViewModels.Admins.MusicAlbums;

public class EditMusicAlbumVM
{
    [Required]
    public long AlbumId { get; set; }

    [Display(Name = "نام آلبوم")]
    [Required(ErrorMessage = "نام آلبوم را وارد کنید")]
    [MaxLength(100, ErrorMessage = "نام آلبوم نباید بیشتر از 100 کارکتر باشد")]
    public string AlbumName { get; set; }

    [Display(Name = "تصویر")]
    public IFormFile? CoverImg { get; set; }

    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "دسته بندی را وارد کنید")]
    public long CategoryId { get; set; }

    [Display(Name = "نوع آلبوم")]
    [Required(ErrorMessage = "نوع آلبوم را وارد کنید")]
    public AlbumType AlbumType { get; set; }

    [Display(Name = "اسلاگ")]
    [Required(ErrorMessage = "اسلاگ را وارد کنید")]
    public string Slug { get; set; }

    [Display(Name = "SEO")]
    public SeoDataViewModel SeoData { get; set; }
}