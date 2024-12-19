using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Proj.Application.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public long UserId { get; set; }
    public IFormFile? Avatar { get;  set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public EditUserCommand(long userId, IFormFile? avatar, string fullName, string email, string phoneNumber)
    {
        UserId = userId;
        Avatar = avatar;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}