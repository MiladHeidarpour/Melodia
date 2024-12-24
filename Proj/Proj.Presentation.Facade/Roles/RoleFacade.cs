using Common.Application;
using MediatR;
using Proj.Application.Roles.Create;
using Proj.Application.Roles.Delete;
using Proj.Application.Roles.Edit;
using Proj.Query.Roles.Dtos;
using Proj.Query.Roles.GetById;
using Proj.Query.Roles.GetList;

namespace Proj.Presentation.Facade.Roles;

public class RoleFacade : IRoleFacade
{
    private readonly IMediator _mediator;

    public RoleFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> CreateRole(CreateRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditRole(EditRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteRole(DeleteRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<RoleDto?> GetRoleById(long roleId)
    {
        return await _mediator.Send(new GetRoleByIdQuery(roleId));
    }

    public async Task<List<RoleDto>> GetRoles()
    {
        return await _mediator.Send(new GetRoleByListQuery());
    }
}