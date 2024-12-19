using Common.Application;
using Common.Application.SecurityUtil;
using Proj.Domain.UserAgg;
using Proj.Domain.UserAgg.Repositories;
using Proj.Domain.UserAgg.Services;

namespace Proj.Application.Users.Create;

internal class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;

    public CreateUserCommandHandler(IUserRepository repository, IUserDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var password = Sha256Hasher.Hash(request.Password);
        var user = new User(request.RoleId, request.PhoneNumber, password, _domainService, request.FullName, request.Email);
        await _repository.AddAsync(user);
        await _repository.Save();
        return OperationResult.Success("کاربر با موفقیت اضافه شد");
    }
}