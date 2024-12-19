using Common.AspNetCore._Utils;
using Microsoft.AspNetCore.Mvc;
using Proj.Api.ViewModels.Admins.Users;
using Proj.Application.Users.Create;
using Proj.Application.Users.Delete;
using Proj.Presentation.Facade.Users;
using Proj.Query.Users.Dtos;
using Proj.Query.Users.DTOs;

namespace Proj.Api.Controllers.AdminControllers;

public class AdminUserController : AdminApiController
{
    private readonly IUserFacade _facade;

    public AdminUserController(IUserFacade facade)
    {
        _facade = facade;
    }

    #region Query

    [HttpGet("Email/{email}")]
    public async Task<ApiResult<UserDto?>> GetUserByEmail(string email)
    {
        var result = await _facade.GetUserByEmail(email);
        return QueryResult(result);
    }

    [HttpGet("GetByFilter")]
    public async Task<ApiResult<UserFilterResult>> GetUserByFilter(UserFilterParams filterParams)
    {
        var result = await _facade.GetUserByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{userId}")]
    public async Task<ApiResult<UserDto?>> GetUserById(long userId)
    {
        var result = await _facade.GetUserById(userId);
        return QueryResult(result);
    }

    [HttpGet("PhoneNumber/{phoneNumber}")]
    public async Task<ApiResult<UserDto?>> GetUserByPhoneNumber(string phoneNumber)
    {
        var result = await _facade.GetUserByPhoneNumber(phoneNumber);
        return QueryResult(result);
    }

    #endregion

    #region Command

    [HttpPost]
    //باید عکس هم اضافه کند
    //تلگرام هم پیام ارسال کند
    public async Task<ApiResult> CreateUser(CreateUserVm command)
    {
        var result = await _facade.CreateUser(new CreateUserCommand()
        {
            RoleId = command.RoleId,
            FullName = command.FullName,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Password = command.Password
        });

        return CommandResult(result);
    }

    [HttpPut]
    public async Task EditUser()
    {
        return ;
    }

    [HttpDelete("{userId}")]
    public async Task<ApiResult> DeleteUser(long userId)
    {
        var result = await _facade.DeleteUser(new DeleteUserCommand(userId));
        return CommandResult(result);
    }

    #endregion
}