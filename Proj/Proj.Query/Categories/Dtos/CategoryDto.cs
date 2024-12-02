using Common.Domain.ValueObjects;
using Common.Query;

namespace Proj.Query.Categories.Dtos;

public class CategoryDto : BaseDto
{
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}