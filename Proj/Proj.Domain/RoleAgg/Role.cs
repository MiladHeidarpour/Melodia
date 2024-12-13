using Common.Domain.Base;
using Common.Domain.Exceptions;

namespace Proj.Domain.RoleAgg;

public class Role:AggregateRoot
{
    public string Title { get; private set; }
    public List<RolePermission> Permissions { get; private set; }

    private Role()
    {

    }
    public Role(string title, List<RolePermission> permissions)
    {
        Title = title;
        Permissions = permissions;
    }
    public void SetPermissions(List<RolePermission> permissions)
    {
        Permissions = permissions;
    }
    public void Edit(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
    }
}