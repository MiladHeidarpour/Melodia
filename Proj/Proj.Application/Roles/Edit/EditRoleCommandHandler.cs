using Common.Application;
using Proj.Domain.RoleAgg;
using Proj.Domain.RoleAgg.Repositories;

namespace Proj.Application.Roles.Edit;

public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
{
    private readonly IRoleRepository _repository;

    public EditRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetTracking(request.Id);
        if (role == null)
        {
            return OperationResult.NotFound();
        }

        role.Edit(request.Title);

        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(f =>
        {
            permissions.Add(new RolePermission(f));
        });

        role.SetPermissions(permissions);
        await _repository.Save();
        return OperationResult.Success();
    }
}