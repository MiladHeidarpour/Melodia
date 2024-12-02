using Common.Query;
using Proj.Query.Categories.Dtos;

namespace Proj.Query.Categories.GetBySlug;

public record GetCategoryBySlugQuery(string Slug) : IQuery<CategoryDto>;