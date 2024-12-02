using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Categories.Edit;

public record EditCategoryCommand(long CategoryId, string Title, IFormFile? ImageName, string Slug, SeoData SeoData) : IBaseCommand;