using Common.Application;
using Proj.Application.Users.AddToken;
using Proj.Application.Users.Register;
using Proj.Application.Users.RemoveToken;
using Proj.Query.Users.Dtos;
using Proj.Query.Users.DTOs;
using Proj.Query.Users.Dtos.UserTokens;
using MediatR;
using Proj.Query.Users.GetByEmail;
using Proj.Query.Users.GetByFilter;
using Proj.Query.Users.GetByPhoneNumber;
using Proj.Query.Users.GetById;
using Common.Application.SecurityUtil;
using Proj.Query.Users.UserTokens.GetByRefreshToken;
using Proj.Query.Users.UserTokens.GetByJwtToken;

namespace Proj.Presentation.Facade.Users;

public class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;

    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
        {
            return OperationResult.Success();
        }
        return OperationResult.Error();
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<UserDto?> GetUserByEmail(string email)
    {
        return await _mediator.Send(new GetUserByEmailQuery(email));
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        return await _mediator.Send(new GetUserByIdQuery(userId));
    }

    public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
    {
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
        return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));
    }

    public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
    {
        var hashJwtToken = Sha256Hasher.Hash(jwtToken);
        return await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }
}