using System.ComponentModel.DataAnnotations;

namespace Proj.Api.ViewModels.Admins.Artists;

public class EditArtistVM
{
    [Required]
    public long ArtistId { get; set; }

    [Display(Name = "نام آرتیست")]
    [Required(ErrorMessage = "نام آرتیست را وارد کنید")]
    [MaxLength(150, ErrorMessage = "نام آرتیست نباید بیشتر از 150 کارکتر باشد")]
    public string ArtistName { get; set; }

    [Display(Name = "تصویر")]
    public IFormFile? ArtistImg { get; set; }

    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "دسته بندی را وارد کنید")]
    public long CategoryId { get; set; }

    [Display(Name = "درباره آرتیست")]
    public string? AboutArtist { get; set; }

    [Display(Name = "اسلاگ")]
    [Required(ErrorMessage = "اسلاگ را وارد نمایید")]
    public string Slug { get; set; }

    [Display(Name = "SEO")]
    public SeoDataViewModel SeoData { get; set; }
}