using Common.Query;
using Proj.Query.Roles.Dtos;

namespace Proj.Query.Roles.GetById;

public record GetRoleByIdQuery(long RoleId) : IQuery<RoleDto>;