using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Roles.Dtos;

namespace Proj.Query.Roles.GetById;

internal class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly ProjContext _context;

    public GetRoleByIdQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == request.RoleId, cancellationToken);
        if (role == null)
        {
            return null;
        }
        return new RoleDto()
        {
            Id = role.Id,
            Title = role.Title,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(s => s.Permission).ToList(),
        };
    }
}