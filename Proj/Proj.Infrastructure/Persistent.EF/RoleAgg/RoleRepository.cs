using Proj.Domain.RoleAgg;
using Proj.Domain.RoleAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.RoleAgg;

public class RoleRepository:BaseRepository<Role>,IRoleRepository
{
    public RoleRepository(ProjContext context) : base(context)
    {
    }
}