using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Proj.Application.Categories.Create;

namespace Proj.Application.Categories.Edit;

public record EditCategoryCommand(long CategoryId, string Title, string Slug, IFormFile ImageName, SeoData SeoData) : IBaseCommand;