﻿using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.AspNetCore.IGapUtil;
using Common.AspNetCore.TelegramUtil;
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
using UAParser;

namespace Proj.Api.Controllers;
public class AuthController : ApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IConfiguration _configuration;
    private readonly ITelegramService _telegramService;
    private readonly IIGapService _iGapService;

    public AuthController(IUserFacade userFacade, IConfiguration configuration, ITelegramService telegramService, IIGapService iGapService)
    {
        _userFacade = userFacade;
        _configuration = configuration;
        _telegramService = telegramService;
        _iGapService = iGapService;
    }

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


        var user = new UserDto();
        if (await _userFacade.GetUserByEmail(loginVm.UserIdentifier) != null)
        {
            user = await _userFacade.GetUserByEmail(loginVm.UserIdentifier);
        }
        else if (await _userFacade.GetUserByPhoneNumber(loginVm.UserIdentifier) != null)
        {
            user = await _userFacade.GetUserByPhoneNumber(loginVm.UserIdentifier);
        }

        if (user == null)
        {
            var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
            return CommandResult(result);
        }

        if (Sha256Hasher.IsCompare(user.Password, loginVm.Password) == false)
        {
            var result = OperationResult<LoginResultDto>.Error("حساب کاربری شما غیرفعال است");
            return CommandResult(result);
        }
        if (user.IsActive == false)
        {
            var result = OperationResult<LoginResultDto>.Error("حساب کاربری شما غیرفعال است");
            return CommandResult(result);
        }
        var loginResult = await AddTokenAndGenerateJwt(user);
        return CommandResult(loginResult);
    }

    [HttpPost("Register")]
    public async Task<ApiResult> Register(RegisterVM registerViewModel)
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
        var command = new RegisterUserCommand(registerViewModel.PhoneNumber, registerViewModel.Password);
        var result = await _userFacade.RegisterUser(command);


        await _telegramService.SendMessage(
            @$"*ملودیا بات*
ادمین گرامی
کاربر جدید با شماره {command.PhoneNumber} ثبت نام کرده است
Telegram : t.me/+98{command.PhoneNumber.Substring(1)}
WhatsApp : wa.me/+98{command.PhoneNumber.Substring(1)}");

        return CommandResult(result);
    }
    [HttpPost("RefreshToken")]
    public async Task<ApiResult<LoginResultDto?>> RefreshToken(string refreshToken)
    {
        var result = await _userFacade.GetUserTokenByRefreshToken(refreshToken);
        if (result == null)
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

    private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
    {
        //var uaParser = Parser.GetDefault();
        //var info = uaParser.Parse(HttpContext.Request.Headers["user-agent"]);
        //var device = $"{info.Device.Family}/{info.OS} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
        //var token = JwtTokenBuilder.BuildToken(user, _configuration);
        //var refreshToken = Guid.NewGuid().ToString();

        //var hashJwt = Sha256Hasher.Hash(token);
        //var hashRefreshToken = Sha256Hasher.Hash(refreshToken);


        //var tokenResult = await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashJwt, hashRefreshToken, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));

        //if (tokenResult.Status != OperationResultStatus.Success)
        //{
        //    return OperationResult<LoginResultDto?>.Error();
        //}

        //return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
        //{
        //    Token = token,
        //    RefreshToken = refreshToken,
        //});



        //test
        var uaParser = Parser.GetDefault();
        var header = HttpContext.Request.Headers["user-agent"].ToString();
        var device = "window";
        if (header != null)
        {
            var info = uaParser.Parse(header);
            device = $"{info.Device.Family}/{info.OS} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
        }

        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        var refreshToken = Guid.NewGuid().ToString();

        var hashJwt = Sha256Hasher.Hash(token);
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);


        var tokenResult = await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashJwt, hashRefreshToken, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));

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
}