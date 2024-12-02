using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Categories.Dtos;
using Proj.Query.Categories.Mapper;

namespace Proj.Query.Categories.GetBySlug;

internal class GetCategoryBySlugQueryHandler : IQueryHandler<GetCategoryBySlugQuery, CategoryDto>
{
    private readonly ProjContext _context;

    public GetCategoryBySlugQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(f => f.Slug == request.Slug, cancellationToken);
        return category.Map();
    }
}