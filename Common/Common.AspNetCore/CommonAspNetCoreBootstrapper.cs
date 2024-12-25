using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Common.AspNetCore._Utils;
using Common.AspNetCore.EmailUtil;
using Common.AspNetCore.IGapUtil;
using Common.AspNetCore.TelegramUtil;
using Microsoft.Extensions.DependencyInjection;

namespace Common.AspNetCore;

public static class CommonAspNetCoreBootstrapper
{
    public static void Init(IServiceCollection services)
    {
        services.AddTransient<IFileService, FileService>();
        services.AddScoped<ITelegramService, TelegramService>();
        services.AddScoped<IIGapService, IGapService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}