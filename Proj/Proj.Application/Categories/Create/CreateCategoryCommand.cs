using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Categories.Create;

public record CreateCategoryCommand(string Title, IFormFile ImageName, string Slug, SeoData SeoData) : IBaseCommand;