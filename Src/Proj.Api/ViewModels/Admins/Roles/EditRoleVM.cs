using Proj.Domain.RoleAgg.Enums;

namespace Proj.Api.ViewModels.Admins.Roles;

public class EditRoleVM
{
    public long RoleId { get; set; }
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}