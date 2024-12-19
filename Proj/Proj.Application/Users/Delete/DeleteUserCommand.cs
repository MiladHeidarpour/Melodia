using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Users.Delete;

public record DeleteUserCommand(long UserId) : IBaseCommand;