using Common.Application;
using Common.AspNetCore._Utils;
using Common.AspNetCore.EmailUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Proj.Api.ViewModels.Admins.Settings;
using Proj.Presentation.Facade.Users;
using Proj.Query.Users.Dtos;

namespace Proj.Api.Controllers.AdminControllers;
public class AdminSettingController : AdminApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IEmailService _emailService;

    public AdminSettingController(IUserFacade userFacade, IEmailService emailService)
    {
        _userFacade = userFacade;
        _emailService = emailService;
    }

    #region Query


    #endregion

    /// <summary>
    /// ارسال ایمیل به کاربران
    /// </summary>
    /// <param name="command">اطلاعات ایمیل</param>
    #region Command

    [HttpPost("EmailMessage")]
    public async Task<ApiResult> EmailMessage(EmailMessageVM command)
    {
        List<UserDto>? users = new List<UserDto>();

        if (command.ForAll == true)
        {
            users = (await _userFacade.GetUserList()).Where(f => f.Email.IsNullOrEmpty() == false).ToList();
        }
        else
        {
            users = command?.Users;
        }

        if (users != null)
        {
            foreach (var item in users)
            {
                await _emailService.SendEmail(item.Email, command.Subject, command.Body);
            }
            return CommandResult(OperationResult.Success());
        }

        return CommandResult(OperationResult.Error("کاربری یافت نشد"));
    }

    #endregion
}