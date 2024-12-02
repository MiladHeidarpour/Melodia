using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Categories.Dtos;
using Proj.Query.Categories.Mapper;

namespace Proj.Query.Categories.GetList;

internal class GetCategoryListQueryHandler : IQueryHandler<GetCategoryListQuery, List<CategoryDto>>
{
    private readonly ProjContext _context;

    public GetCategoryListQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.OrderByDescending(c => c.CreationDate).ToListAsync(cancellationToken);
        return categories.Map();
    }
}