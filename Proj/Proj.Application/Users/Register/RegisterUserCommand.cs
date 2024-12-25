using Common.Application;

namespace Proj.Application.Users.Register;

public class RegisterUserCommand:IBaseCommand
{
    public RegisterUserCommand(string userIdentifier, string password)
    {
        UserIdentifier = userIdentifier;
        Password = password;
    }

    public string UserIdentifier { get; set; }
    public string Password { get; set; }
}