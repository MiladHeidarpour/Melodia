﻿using Common.Domain.Repositories;

namespace Proj.Domain.RoleAgg.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<bool> DeleteRole(long roleId);
}