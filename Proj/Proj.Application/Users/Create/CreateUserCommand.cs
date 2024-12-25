using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Users.Create;

public class CreateUserCommand : IBaseCommand
{
    public long RoleId { get; set; }
    public string FullName { get; set; }
    public IFormFile? Avatar { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}