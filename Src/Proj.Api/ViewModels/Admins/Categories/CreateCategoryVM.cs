namespace Proj.Api.ViewModels.Admins.Categories;

public class CreateCategoryVM
{
    public string Title { get; set; }
    public IFormFile ImageName { get; set; }
    public string Slug { get; set; }
    public SeoDataViewModel SeoData { get; set; }
}