using Common.Application;
using Common.Application.SecurityUtil;
using Proj.Domain.RoleAgg.Repositories;
using Proj.Domain.UserAgg;
using Proj.Domain.UserAgg.Repositories;
using Proj.Domain.UserAgg.Services;

namespace Proj.Application.Users.Register;

internal class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserDomainService _domainService;
    private readonly IUserRepository _repository;

    public RegisterUserCommandHandler(IUserDomainService domainService, IUserRepository repository, IRoleRepository roleRepository)
    {
        _domainService = domainService;
        _repository = repository;
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var password = Sha256Hasher.Hash(request.Password);
        var userRole = await _roleRepository.GetByFunc(f => f.Title == "User");
        var user = User.RegisterUser(userRole.Id, request.PhoneNumber, password, _domainService);
        await _repository.AddAsync(user);
        await _repository.Save();
        return OperationResult.Success("ثبت نام با موفقیت انجام شد");
    }
}