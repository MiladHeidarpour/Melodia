using System.ComponentModel.DataAnnotations;

namespace Proj.Api.ViewModels.Admins.Categories;

public class EditCategoryVM
{
    [Required]
    public long CategoryId { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "عنوان را وارد کنید")]
    [MaxLength(100, ErrorMessage = "عنوان نباید بیشتر از 100 کارکتر باشد")]
    public string Title { get; set; }

    [Display(Name = "تصویر")]
    public IFormFile? ImageName { get; set; }

    [Display(Name = "اسلاگ")]
    [Required(ErrorMessage = "اسلاگ را وارد کنید")]
    public string Slug { get; set; }

    [Display(Name = "SEO")]
    public SeoDataViewModel SeoData { get; set; }
}