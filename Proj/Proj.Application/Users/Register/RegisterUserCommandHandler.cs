using Common.Application;
using Common.Application.SecurityUtil;
using Common.Domain;
using Proj.Domain.RoleAgg.Repositories;
using Proj.Domain.UserAgg;
using Proj.Domain.UserAgg.Repositories;
using Proj.Domain.UserAgg.Services;
using System.Text.RegularExpressions;

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
        User user;
        if (EmailValidation.IsValidEmail(request.UserIdentifier))
        {
            user = User.RegisterUserEmail(userRole.Id, request.UserIdentifier, password, _domainService);
        }
        else
        {
            user = User.RegisterUserPhoneNumber(userRole.Id, request.UserIdentifier, password, _domainService);
        }
        await _repository.AddAsync(user);
        await _repository.Save();
        return OperationResult.Success("ثبت نام با موفقیت انجام شد");
    }
}