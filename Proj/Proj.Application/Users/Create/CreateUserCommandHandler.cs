using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Proj.Application._Utilities;
using Proj.Domain.UserAgg;
using Proj.Domain.UserAgg.Repositories;
using Proj.Domain.UserAgg.Services;

namespace Proj.Application.Users.Create;

internal class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;
    private readonly IFileService _fileService;

    public CreateUserCommandHandler(IUserRepository repository, IUserDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var password = Sha256Hasher.Hash(request.Password);
        var user = new User(request.RoleId, password,  _domainService, request.PhoneNumber, request.Email, request.FullName);
        await _repository.AddAsync(user);

        if (request.Avatar != null)
        {
            var avatar = await _fileService.SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
            user.SetAvatar(avatar);
        }

        await _repository.Save();
        return OperationResult.Success("کاربر با موفقیت اضافه شد");
    }
}