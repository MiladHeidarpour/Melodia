﻿using Common.Application;
using Proj.Application.Roles.Create;
using Proj.Application.Roles.Delete;
using Proj.Application.Roles.Edit;
using Proj.Query.Roles.Dtos;

namespace Proj.Presentation.Facade.Roles;

public interface IRoleFacade
{
    //Command
    Task<OperationResult> CreateRole(CreateRoleCommand command);
    Task<OperationResult> EditRole(EditRoleCommand command);
    Task<OperationResult> DeleteRole(DeleteRoleCommand command);


    //Query
    Task<RoleDto?> GetRoleById(long roleId);
    Task<List<RoleDto>> GetRoles();
}