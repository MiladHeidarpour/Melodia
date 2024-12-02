using Common.Query;
using Proj.Query.Categories.Dtos;

namespace Proj.Query.Categories.GetList;

public record GetCategoryListQuery : IQuery<List<CategoryDto>>;