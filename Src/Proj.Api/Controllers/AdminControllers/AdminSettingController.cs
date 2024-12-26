using Common.Application;
using Common.AspNetCore._Utils;
using Common.AspNetCore.EmailUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Proj.Api.ViewModels.Admins.Settings;
using Proj.Presentation.Facade.Users;

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

    [HttpPost("EmailMessage")]
    public async Task<ApiResult> EmailMessage(EmailMessageVM command)
    {
        var usersEmails = (await _userFacade.GetUserList()).Where(f => f.Email.IsNullOrEmpty() == false);

        foreach (var user in usersEmails)
        {
            await _emailService.SendEmail(user.Email, command.Subject, command.Body);
        }
        return CommandResult(OperationResult.Success());
    }
}