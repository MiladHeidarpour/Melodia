using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Common.AspNetCore.MiddleWares;


public static class GetOnlineUserHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseUseGetOnlineUserHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseGetOnlineUser>();
    }
}
public class UseGetOnlineUser
{
    private static HashSet<string> OnlineUsers = new HashSet<string>();
    private readonly RequestDelegate _next;

    public UseGetOnlineUser(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var userId = context.User.Identity.IsAuthenticated
            ? context.User.Identity.Name
            : context.Connection.LocalIpAddress.ToString();

        OnlineUsers.Add(userId);
        await _next(context);

        // حذف کاربر بعد از پایان درخواست
        //OnlineUsers.Remove(userId);
    }

    public static int GetOnlineUsersCount()
    {
        return OnlineUsers.Count;
    }
}