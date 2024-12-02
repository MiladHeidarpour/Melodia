using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Categories.Dtos;
using Proj.Query.Categories.Mapper;

namespace Proj.Query.Categories.GetById;

internal class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ProjContext _context;

    public GetCategoryByIdQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(f => f.Id == request.CategoryId, cancellationToken);
        return category.Map();
    }
}