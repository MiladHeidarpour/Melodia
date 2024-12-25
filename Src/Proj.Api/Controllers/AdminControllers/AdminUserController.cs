using Common.Application;
using Common.AspNetCore._Utils;
using Common.AspNetCore.TelegramUtil;
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
    private readonly ITelegramService _telegramService;

    public AdminUserController(IUserFacade facade, ITelegramService telegramService)
    {
        _facade = facade;
        _telegramService = telegramService;
    }

    #region Query

    /// <summary>
    /// جستوجوی کاربر بر اساس شناسه ایمیل
    /// </summary>
    /// <param name="email">شناسه ایمیل</param>
    /// <returns>کاربر</returns>
    [HttpGet("Email/{email}")]
    public async Task<ApiResult<UserDto?>> GetUserByEmail(string email)
    {
        var result = await _facade.GetUserByEmail(email);
        return QueryResult(result);
    }



    /// <summary>
    /// جستوجوی کاربر بر اساس فیلتر
    /// </summary>
    /// <param name="filterParams">مقادیر جستوجو</param>
    /// <returns>کاربر های فیلتر شده</returns>
    [HttpGet("GetByFilter")]
    public async Task<ApiResult<UserFilterResult>> GetUserByFilter(UserFilterParams filterParams)
    {
        var result = await _facade.GetUserByFilter(filterParams);
        return QueryResult(result);
    }



    /// <summary>
    /// جستوجوی کاربر بر اساس شناسه کاربر
    /// </summary>
    /// <param name="userId">شناسه کاربر</param>
    /// <returns>کاربر</returns>
    [HttpGet("{userId}")]
    public async Task<ApiResult<UserDto?>> GetUserById(long userId)
    {
        var result = await _facade.GetUserById(userId);
        return QueryResult(result);
    }



    /// <summary>
    /// جستوجوی کاربر بر اساس شناسه تلفن
    /// </summary>
    /// <param name="phoneNumber">شناسه تلفن</param>
    /// <returns>کاربر</returns>
    [HttpGet("PhoneNumber/{phoneNumber}")]
    public async Task<ApiResult<UserDto?>> GetUserByPhoneNumber(string phoneNumber)
    {
        var result = await _facade.GetUserByPhoneNumber(phoneNumber);
        return QueryResult(result);
    }

    #endregion

    #region Command

    /// <summary>
    /// ثبت کاربر
    /// </summary>
    /// <param name="command">اطلاعات کاربر</param>
    [HttpPost]
    public async Task<ApiResult> CreateUser([FromForm] CreateUserVm command)
    {
        var result = await _facade.CreateUser(new CreateUserCommand()
        {
            RoleId = command.RoleId,
            FullName = command.FullName,
            Avatar = command.Avatar,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Password = command.Password
        });

        if (result.Status == OperationResultStatus.Success)
        {
            await _telegramService.SendMessage(
                @$"🎵ملودیا بات🎵
ادمین گرامی
🙎‍♀️کاربر جدیدی ثبت نام کرده است🙎‍♂️
تاریخ : {DateTime.Now.ToPersianDateAndTime("ds dd ms Y")}
PhoneNumber : {command.PhoneNumber}
Telegram : t.me/+98{command.PhoneNumber.Substring(1)}
WhatsApp : wa.me/+98{command.PhoneNumber.Substring(1)}");
        }

        return CommandResult(result);
    }



    /// <summary>
    /// ویرایش کاربر
    /// </summary>
    /// <param name="command">اطلاعات کاربر</param>
    [HttpPut]
    public async Task EditUser()
    {
        return;
    }


    /// <summary>
    /// حذف کاربر
    /// </summary>
    /// <param name="userId">شناسه کاربر</param>
    [HttpDelete("{userId}")]
    public async Task<ApiResult> DeleteUser(long userId)
    {
        var result = await _facade.DeleteUser(new DeleteUserCommand(userId));
        return CommandResult(result);
    }

    #endregion
}