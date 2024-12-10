using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Categories.Create;

public class CreateCategoryCommand : IBaseCommand
{
    public string Title { get; set; }
    public IFormFile ImageName { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}