using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Categories.Edit;

public class EditCategoryCommand : IBaseCommand
{
    public long CategoryId { get; set; }
    public string Title { get; set; }
    public IFormFile? ImageName { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}