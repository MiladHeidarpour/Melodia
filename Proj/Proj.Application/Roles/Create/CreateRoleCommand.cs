using Common.Application;
using Proj.Domain.RoleAgg.Enums;

namespace Proj.Application.Roles.Create;

public record CreateRoleCommand(string Title, List<Permission> Permissions) : IBaseCommand;