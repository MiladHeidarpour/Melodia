using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore._Utils;
using Common.AspNetCore.IGapUtil;
using Common.AspNetCore.TelegramUtil;
using Common.Domain.Utilities;
using DeviceDetectorNET;
using Kavenegar;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj.Api.Infrastructure.JwtUtils;
using Proj.Api.ViewModels.Auth;
using Proj.Api.ViewModels.Auth.Dtos;
using Proj.Application.Users.AddToken;
using Proj.Application.Users.Register;
using Proj.Application.Users.RemoveToken;
using Proj.Presentation.Facade.Users;
using Proj.Query.Users.Dtos;
using Common.AspNetCore.EmailUtil;
using Common.Domain;
using Proj.Application.Verifications.Create;
using Proj.Application.Verifications.Delete;
using Proj.Presentation.Facade.Verifications;
using UAParser;

namespace Proj.Api.Controllers;
public class AuthController : ApiController
{
    private readonly IVerificationFacade _verificationFacade;
    private readonly IUserFacade _userFacade;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly ITelegramService _telegramService;
    //private readonly IIGapService _iGapService;
    //private readonly string _apiKey;

    public AuthController(IUserFacade userFacade, IConfiguration configuration, IEmailService emailService, ITelegramService telegramService, IVerificationFacade verificationFacade /*, IIGapService iGapService*//*, IOptions<KavenegarSettings> settings*/)
    {
        _userFacade = userFacade;
        _configuration = configuration;
        _emailService = emailService;
        _telegramService = telegramService;
        _verificationFacade = verificationFacade;
        //_iGapService = iGapService;
        //_apiKey = settings.Value.ApiKey;
    }

    #region Command

    /// <summary>
    /// ثبت نام با کد تایید
    /// </summary>
    /// <param name="userIdentifier">اطلاعات کاربر</param>
    [HttpPost("RegisterCode")]
    public async Task<ApiResult<long?>> RegisterCode(string userIdentifier)
    {
        var user = await _userFacade.GetUserByPhoneNumber(userIdentifier) ??
                   await _userFacade.GetUserByEmail(userIdentifier);
        if (user != null)
        {
            return CommandResult(OperationResult<long?>.Error("کاربر از قبل ثبت نام کرده است"));
        }

        //ToDo
        //if the userIdentifier is exist in verification dbo first delete the last code and then generate new code


        // ارسال کد ورود به ایمیل
        if (EmailValidation.IsValidEmail(userIdentifier))
        {
            var verificationId = await GenerateCodeAndSet(userIdentifier);

            var verificationInfo = await _verificationFacade.GetVerificationById(verificationId);

            var subject = "کد تایید ثبت نام در ملودیا";
            var message = $"کد تایید شما : {verificationInfo.VerificationCode}";
            await _emailService.SendEmail(userIdentifier, subject, message);

            return new ApiResult<long?>()
            {
                Data = verificationId,
                IsSuccess = true,
                MetaData = new MetaData()
                {
                    Message = "کد با موفقیت ارسال شد",
                    AppStatusCode = AppStatusCode.Success,
                }
            };
        }
        if (string.IsNullOrWhiteSpace(userIdentifier) && userIdentifier.IsText() && userIdentifier.Length == 11)
        {
            var verificationId = await GenerateCodeAndSet(userIdentifier);

            var verificationInfo = await _verificationFacade.GetVerificationById(verificationId);

            return new ApiResult<long?>()
            {
                Data = verificationId,
                IsSuccess = true,
                MetaData = new MetaData()
                {
                    Message = "کد با موفقیت ارسال شد",
                    AppStatusCode = AppStatusCode.Success,
                }
            };
        }

        return CommandResult(OperationResult<long?>.Error("شماره تلفن یا ایمیل نامعتبر است"));
    }



    /// <summary>
    /// تایید کد ورود
    /// </summary>
    /// <param name="code">کد تایید</param>
    [HttpPost("Verify")]
    public async Task<ApiResult<long?>> Verify(string code, long verificationId)
    {
        var verificationInfo = await _verificationFacade.GetVerificationById(verificationId);

        if (verificationInfo == null)
        {
            return CommandResult(OperationResult<long?>.Error("کد وارد شده معتبر نیست"));
        }

        if (verificationInfo.ExpirationTime > DateTime.UtcNow && verificationInfo.IsVerified == false)
        {
            if (code == verificationInfo.VerificationCode)
            {
                await _verificationFacade.VerifyCode(verificationId);

                return new ApiResult<long?>()
                {
                    Data = verificationId,
                    IsSuccess = true,
                    MetaData = new MetaData()
                    {
                        Message = "کد وارد شده صحیح است",
                        AppStatusCode = AppStatusCode.Success,
                    }
                };
            }
        }
        else
        {
            await _verificationFacade.Delete(new DeleteVerificationCommand(verificationId));
        }

        return new ApiResult<long?>()
        {
            Data = null,
            IsSuccess = false,
            MetaData = new MetaData()
            {
                Message = "کد وارد شده معتبر نیست",
                AppStatusCode = AppStatusCode.BadRequest,
            }
        };
    }

    /// <summary>
    /// ارسال کد مجدد
    /// </summary>
    /// <param name="verificationId">اطلاعات کاربر</param>
    [HttpPost("ResendCode")]
    public async Task<ApiResult<long?>> ResendCode(long verificationId)
    {
        var verificationInfo = await _verificationFacade.GetVerificationById(verificationId);

        if (verificationInfo == null)
        {
            return CommandResult(OperationResult<long?>.Error("دوباره تلاش کنید"));
        }

        // ارسال کد ورود به ایمیل
        if (EmailValidation.IsValidEmail(verificationInfo.UserIdentifier))
        {
            var newVerificationId = await GenerateCodeAndSet(verificationInfo.UserIdentifier);
            var newVerificationInfo = await _verificationFacade.GetVerificationById(newVerificationId);

            await _verificationFacade.Delete(new DeleteVerificationCommand(verificationId));

            var subject = "کد تایید ثبت نام در ملودیا";
            var message = $"کد تایید شما : {newVerificationInfo.VerificationCode}";
            await _emailService.SendEmail(newVerificationInfo.UserIdentifier, subject, message);

            return new ApiResult<long?>()
            {
                Data = newVerificationId,
                IsSuccess = true,
                MetaData = new MetaData()
                {
                    Message = "کد با موفقیت ارسال شد",
                    AppStatusCode = AppStatusCode.Success,
                }
            };
        }
        if (string.IsNullOrWhiteSpace(verificationInfo.UserIdentifier) && verificationInfo.UserIdentifier.IsText() && verificationInfo.UserIdentifier.Length == 11)
        {
            var newVerificationId = await GenerateCodeAndSet(verificationInfo.UserIdentifier);
            var newVerificationInfo = await _verificationFacade.GetVerificationById(newVerificationId);

            await _verificationFacade.Delete(new DeleteVerificationCommand(verificationId));

            //ToDo 
            //برای شماره تلفن سامانه پیامکی ثبت نشده است
            return new ApiResult<long?>()
            {
                Data = newVerificationId,
                IsSuccess = true,
                MetaData = new MetaData()
                {
                    Message = "کد با موفقیت ارسال شد",
                    AppStatusCode = AppStatusCode.Success,
                }
            };
        }

        return CommandResult(OperationResult<long?>.Error("شماره تلفن یا ایمیل نامعتبر است"));
    }


    /// <summary>
    /// ثبت نام کاربر
    /// </summary>
    /// <param name="registerViewModel">اطلاعات کاربر</param>
    [HttpPost("Active")]
    public async Task<ApiResult> Active(RegisterVM registerViewModel, long verificationId)
    {
        if (ModelState.IsValid == false)
        {
            return new ApiResult()
            {
                IsSuccess = false,
                MetaData = new MetaData()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = JoinErrors(),
                }
            };
        }

        var verificationInfo = await _verificationFacade.GetVerificationById(verificationId);
        if (verificationInfo == null && verificationInfo.IsVerified == false)
        {
            return new ApiResult()
            {
                IsSuccess = false,
                MetaData = new MetaData()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = "کد نامعتبر است",
                }
            };
        }

        var command = new RegisterUserCommand(verificationInfo.UserIdentifier, registerViewModel.Password);
        var result = await _userFacade.RegisterUser(command);
        
        await _verificationFacade.Delete(new DeleteVerificationCommand(verificationId));

        if (result.Status == OperationResultStatus.Success)
        {
            try
            {
                if (EmailValidation.IsValidEmail(command.UserIdentifier))
                {
                    await _telegramService.SendMessage(
                        @$"🎵ملودیا بات🎵
ادمین گرامی
🙎‍♀️کاربر جدیدی ثبت نام کرده است🙎‍♂️
تاریخ : {DateTime.Now.ToPersianDateAndTime("ds dd ms Y")}
Email : {command.UserIdentifier}");
                }

                else
                {
                    await _telegramService.SendMessage(
                        @$"🎵ملودیا بات🎵
ادمین گرامی
🙎‍♀️کاربر جدیدی ثبت نام کرده است🙎‍♂️
تاریخ : {DateTime.Now.ToPersianDateAndTime("ds dd ms Y")}
PhoneNumber : {command.UserIdentifier}
Telegram : t.me/+98{command.UserIdentifier.Substring(1)}
WhatsApp : wa.me/+98{command.UserIdentifier.Substring(1)}");
                }
            }
            catch (Exception e)
            {
                //
            }

        }

        return CommandResult(result);
    }



    /// <summary>
    /// ورود کاربر
    /// </summary>
    /// <param name="loginVm">اطلاعات کاربر</param>
    [HttpPost("Login")]
    public async Task<ApiResult<LoginResultDto>> Login(LoginVM loginVm)
    {
        if (ModelState.IsValid == false)
        {
            return new ApiResult<LoginResultDto?>()
            {
                Data = null,
                IsSuccess = false,
                MetaData = new MetaData()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = JoinErrors(),
                }
            };
        }

        var user = await _userFacade.GetUserByPhoneNumber(loginVm.UserIdentifier) ??
                   await _userFacade.GetUserByEmail(loginVm.UserIdentifier);

        if (user is null)
        {
            var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
            return CommandResult(result);
        }
        if (Sha256Hasher.IsCompare(user.Password, loginVm.Password) == false)
        {
            var result = OperationResult<LoginResultDto>.Error("حساب کاربری شما غیرفعال است");
            return CommandResult(result);
        }
        if (user.IsActive is false)
        {
            var result = OperationResult<LoginResultDto>.Error("حساب کاربری شما غیرفعال است");
            return CommandResult(result);
        }
        var loginResult = await AddTokenAndGenerateJwt(user);
        return CommandResult(loginResult);
    }



    /// <summary>
    /// رفرش توکن
    /// </summary>
    /// <param name="refreshToken">اطلاعات توکن</param>
    [HttpPost("RefreshToken")]
    public async Task<ApiResult<LoginResultDto?>> RefreshToken(string refreshToken)
    {
        var result = await _userFacade.GetUserTokenByRefreshToken(refreshToken);
        if (result is null)
        {
            return CommandResult(OperationResult<LoginResultDto>.NotFound());
        }
        if (result.TokenExpireDate > DateTime.Now)
        {
            return CommandResult(OperationResult<LoginResultDto?>.Error("توکن هنوز منقضی نشده است"));
        }
        if (result.RefreshTokenExpireDate < DateTime.Now)
        {
            return CommandResult(OperationResult<LoginResultDto?>.Error("زمان رفرش توکن به پایان رسیده است"));
        }
        await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));

        var user = await _userFacade.GetUserById(result.UserId);
        var loginResult = await AddTokenAndGenerateJwt(user);

        return CommandResult(loginResult);
    }



    /// <summary>
    /// خروج کاربر
    /// </summary>
    [Authorize]
    [HttpDelete("Logout")]
    public async Task<ApiResult> LogOut()
    {
        //var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("bearer","");
        var token = await HttpContext.GetTokenAsync("access_token");
        var result = await _userFacade.GetUserTokenByJwtToken(token);
        if (result == null)
        {
            return CommandResult(OperationResult.NotFound());
        }
        await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));

        return CommandResult(OperationResult.Success());
    }



    /// <summary>
    /// ساخت کد تایید و ارسال آن
    /// </summary>
    private async Task<long> GenerateCodeAndSet(string userIdentifier)
    {
        var verificationCode = TextHelper.GenerateCode(6);

        var setVerification = await _verificationFacade.Create(new CreateVerificationCommand()
        {
            UserIdentifier = userIdentifier,
            VerificationCode = verificationCode,
            ExpirationTime = DateTime.UtcNow.AddMinutes(1).AddSeconds(30),
            IsVerified = false,
        });

        return setVerification.Data;
    }



    /// <summary>
    /// ساهت توکن کاربر
    /// </summary>
    private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
    {
        //var uaParser = Parser.GetDefault();
        //var header = HttpContext.Request.Headers["user-agent"].ToString();
        //var deviceInfo = "window";
        //if (header != null)
        //{
        //    var info = uaParser.Parse(header);
        //    deviceInfo = $"{info.Device.Family}/{info.OS} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
        //}

        var header = HttpContext.Request.Headers["User-Agent"].ToString();
        var deviceInfo = "unknown";

        if (!string.IsNullOrEmpty(header))
        {
            var deviceDetector = new DeviceDetector(header);
            deviceDetector.Parse();

            if (deviceDetector.IsParsed())
            {
                var os = deviceDetector.GetOs();
                var client = deviceDetector.GetClient();
                var deviceType = deviceDetector.GetDeviceName();

                deviceInfo = $"{deviceType}/{os.Match.Name} {os.Match.Version} - {client.Match.Name}";
            }
        }

        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        var refreshToken = Guid.NewGuid().ToString();

        var hashJwt = Sha256Hasher.Hash(token);
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);


        var tokenResult = await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashJwt, hashRefreshToken, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), deviceInfo));

        if (tokenResult.Status != OperationResultStatus.Success)
        {
            return OperationResult<LoginResultDto?>.Error();
        }

        return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
        {
            Token = token,
            RefreshToken = refreshToken,
        });
    }

    #endregion
}