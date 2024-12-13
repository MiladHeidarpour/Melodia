using Common.Application;
using Common.Domain.ValueObjects;

namespace Proj.Application.Users.Register;

public class RegisterUserCommand:IBaseCommand
{
    public RegisterUserCommand(PhoneNumber phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public PhoneNumber PhoneNumber { get; set; }
    public string Password { get; set; }
}