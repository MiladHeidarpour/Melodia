using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Roles.Dtos;

namespace Proj.Query.Roles.GetList;

public class GetRoleByListQuery : IQuery<List<RoleDto>>
{
}

internal class GetRoleByListQueryHandler : IQueryHandler<GetRoleByListQuery, List<RoleDto>>
{
    private readonly ProjContext _context;

    public GetRoleByListQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<List<RoleDto>> Handle(GetRoleByListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(s => new RoleDto()
        {
            Id = s.Id,
            CreationDate = s.CreationDate,
            Permissions = s.Permissions.Select(s => s.Permission).ToList(),
            Title = s.Title,
        }).ToListAsync(cancellationToken);
    }
}