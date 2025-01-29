using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Users.Dtos;
using Proj.Query.Users.Mapper;

namespace Proj.Query.Users.GetList;

internal class GetUserListQueryHandler : IQueryHandler<GetUserListQuery, List<UserDto?>>
{
    private readonly ProjContext _context;

    public GetUserListQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto?>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var userRole = await _context.Roles.FirstOrDefaultAsync(f => f.Title == "User", cancellationToken);
        var user = await _context.Users.Where(f => f.RoleId == userRole.Id).OrderByDescending(f => f.CreationDate).ToListAsync(cancellationToken);
        return user.Map();
    }
}