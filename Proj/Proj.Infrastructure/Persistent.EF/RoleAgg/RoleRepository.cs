using Microsoft.EntityFrameworkCore;
using Proj.Domain.RoleAgg;
using Proj.Domain.RoleAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.RoleAgg;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ProjContext context) : base(context)
    {
    }

    public async Task<bool> DeleteRole(long roleId)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == roleId);
        if (role == null)
        {
            return false;
        }
        _context.Roles.Remove(role);
        return true;
    }
}