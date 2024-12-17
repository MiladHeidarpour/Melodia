using Common.Domain.Base;
using Proj.Domain.RoleAgg.Enums;

namespace Proj.Domain.RoleAgg;

public class RolePermission:BaseEntity
{
    public long RoleId { get; internal set; }
    public Permission Permission { get; private set; }
    public RolePermission(Permission permission)
    {
        Permission = permission;
    }
}