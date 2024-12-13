using Common.Application;
using Shop.Domain.RoleAgg.Enums;

namespace Proj.Application.Roles.Edit;

public record EditRoleCommand(long Id, string Title, List<Permission> Permissions) : IBaseCommand;