using Common.Query;
using Shop.Domain.RoleAgg.Enums;

namespace Proj.Query.Roles.Dtos;

public class RoleDto:BaseDto
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}