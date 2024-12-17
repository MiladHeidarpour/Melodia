using Common.Application;
using Common.Domain.ValueObjects;

namespace Proj.Application.Users.Register;

public class RegisterUserCommand:IBaseCommand
{
    public RegisterUserCommand(string phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}