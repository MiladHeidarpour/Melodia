using Common.Query;
using Proj.Query.Categories.Dtos;

namespace Proj.Query.Categories.GetById;

public record GetCategoryByIdQuery(long CategoryId) : IQuery<CategoryDto>;