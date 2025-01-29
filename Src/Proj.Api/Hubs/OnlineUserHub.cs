using Microsoft.AspNetCore.SignalR;
using Proj.Query.Users.Dtos;

namespace Proj.Api.Hubs;

public class OnlineUserHub : Hub
{
    List<UserDto> Users = new List<UserDto>();
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}