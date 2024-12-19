using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.UserAgg.Repositories;
using Proj.Domain.UserAgg.Services;

namespace Proj.Application.Users.Delete;

internal class DeleteUserCommandHandler : IBaseCommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;
    private readonly IFileService _fileService;

    public DeleteUserCommandHandler(IUserRepository repository, IUserDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }


    public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteUser(request.UserId);
        if (result==false)
        {
            return OperationResult.Error("عملیات با شکست مواجه شد");
        }
        await _repository.Save();
        return OperationResult.Success("کاربر با موفقیت حذف شد");
    }
}