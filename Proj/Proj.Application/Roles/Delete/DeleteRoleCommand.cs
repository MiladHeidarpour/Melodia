using Common.Application;
using Proj.Domain.RoleAgg.Repositories;

namespace Proj.Application.Roles.Delete;

public record DeleteRoleCommand(long RoleId) : IBaseCommand;


internal class DeleteRoleCommandHandler : IBaseCommandHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _repository;

    public DeleteRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteRole(request.RoleId);
        if (result == false)
        {
            return OperationResult.Error("عملیات با شکست مواجه شد");
        }
        await _repository.Save();
        return OperationResult.Success("نقش با موفقیت حذف شد");
    }
}