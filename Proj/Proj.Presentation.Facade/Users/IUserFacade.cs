using Common.Application;
using Proj.Application.Users.AddToken;
using Proj.Application.Users.Create;
using Proj.Application.Users.Delete;
using Proj.Application.Users.Edit;
using Proj.Application.Users.Register;
using Proj.Application.Users.RemoveToken;
using Proj.Query.Users.Dtos.UserTokens;
using Proj.Query.Users.Dtos;
using Proj.Query.Users.DTOs;

namespace Proj.Presentation.Facade.Users;

public interface IUserFacade
{
    //Command
    Task<OperationResult> CreateUser(CreateUserCommand command);
    Task<OperationResult> EditUser(EditUserCommand command);
    Task<OperationResult> DeleteUser(DeleteUserCommand command);
    Task<OperationResult> RegisterUser(RegisterUserCommand command);
    Task<OperationResult> AddToken(AddUserTokenCommand command);
    Task<OperationResult> RemoveToken(RemoveUserTokenCommand command);

    //Query
    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
    Task<UserDto?> GetUserByEmail(string email);
    Task<UserDto?> GetUserById(long userId);
    Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken);
    Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken);
    Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
}