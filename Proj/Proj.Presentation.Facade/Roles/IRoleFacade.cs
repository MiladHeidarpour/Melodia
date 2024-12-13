using Common.Application;
using Proj.Application.Roles.Create;
using Proj.Application.Roles.Edit;
using Proj.Query.Roles.Dtos;

namespace Proj.Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult> CreateRole(CreateRoleCommand command);
    Task<OperationResult> EditRole(EditRoleCommand command);

    Task<RoleDto?> GetRoleById(long roleId);
    Task<List<RoleDto>> GetRoles();
}