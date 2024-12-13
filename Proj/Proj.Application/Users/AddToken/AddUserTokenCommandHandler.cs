using Common.Application;
using Proj.Domain.UserAgg.Repositories;

namespace Proj.Application.Users.AddToken;

internal class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository _repository;

    public AddUserTokenCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);

        if (user == null)
        {
            return OperationResult.NotFound("کاربر مورد نظر یافت نشد");
        }
        user.AddToken(request.HashJwtToken, request.HashRefreshToken, request.TokenExpireDate, request.RefreshTokenExpireDate, request.Device);
        await _repository.Save();
        return OperationResult.Success();
    }
}