using Common.Application;
using Common.AspNetCore;
using Common.AspNetCore._Utils;
using Common.AspNetCore.EmailUtil;
using Common.AspNetCore.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Proj.Api.Hubs;
using Proj.Api.Infrastructure;
using Proj.Api.Infrastructure.JwtUtils;
using Proj.Config;
using SwaggerThemes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
{
    option.InvalidModelStateResponseFactory = (context =>
    {
        var result = new ApiResult()
        {
            IsSuccess = false,
            MetaData = new()
            {
                AppStatusCode = AppStatusCode.BadRequest,
                Message = ModelStateUtil.GetModelStateError(context.ModelState),
            }
        };
        return new BadRequestObjectResult(result);
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Melodia.Api.xml"), true);
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Melodia.Api",
        Description = "پروژه تمرینی سایت موزیک"
    });
    JwtLogin.JwtLoginConfig(option);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.RegisterProjDependency(connectionString);
builder.Services.RegisterApiDependency();

CommonBootstrapper.Init(builder.Services);
CommonAspNetCoreBootstrapper.Init(builder.Services);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.Configure<KavenegarSettings>(builder.Configuration.GetSection("Kavenegar"));
builder.Services.AddSignalR();

//builder.Services.AddDistributedMemoryCache(); // <-- این خط ارائه‌دهنده حافظه موقت را اضافه می‌کند
//builder.Services.AddSession(options =>
//{
//    // تنظیم تایمر انقضای Session (به عنوان مثال 2 دقیقه)
//    options.IdleTimeout = TimeSpan.FromMinutes(2);
//    options.Cookie.HttpOnly = true; // امنیت بیشتر
//    options.Cookie.IsEssential = true; // برای GDPR ضروری است
//});// فعال‌سازی Session

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(Theme.Vs2022);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("MelodiaApi");
//app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseApiCustomExceptionHandler();
//app.UseUseGetOnlineUserHandler();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<OnlineUserHub>("/ChatHub");
});

app.Run();
