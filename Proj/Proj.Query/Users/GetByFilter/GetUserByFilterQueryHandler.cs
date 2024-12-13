using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Users.DTOs;
using Proj.Query.Users.Mapper;

namespace Proj.Query.Users.GetByFilter;

internal class GetUserByFilterQueryHandler : IQueryHandler<GetUserByFilterQuery, UserFilterResult>
{
    private readonly ProjContext _context;

    public GetUserByFilterQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterParams = request.FilterParams;
        var result = _context.Users.OrderByDescending(r => r.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterParams.FullName))
        {
            result = result.Where(f => f.FullName.Contains(filterParams.FullName));
        }
        if (!string.IsNullOrWhiteSpace(filterParams.Email))
        {
            result = result.Where(f => f.Email.Contains(filterParams.Email));
        }
        if (!string.IsNullOrWhiteSpace(filterParams.PhoneNumber))
        {
            result = result.Where(f => f.PhoneNumber.Contains(filterParams.PhoneNumber));
        }
        if (filterParams.Id != null)
        {
            result = result.Where(f => f.Id == filterParams.Id);
        }
        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new UserFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take).Select(user => user.MapFilterData()).ToListAsync(cancellationToken),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }
}