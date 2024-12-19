using Common.Application;
using Common.AspNetCore;
using Common.AspNetCore._Utils;
using Common.AspNetCore.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Proj.Api.Infrastructure;
using Proj.Api.Infrastructure.JwtUtils;
using Proj.Config;

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
    JwtLogin.JwtLoginConfig(option);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.RegisterProjDependency(connectionString);
builder.Services.RegisterApiDependency();

CommonBootstrapper.Init(builder.Services);
CommonAspNetCoreBootstrapper.Init(builder.Services);
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("MelodiaApi");
app.UseAuthentication();
app.UseAuthorization();
app.UseApiCustomExceptionHandler();
app.MapControllers();

app.Run();
