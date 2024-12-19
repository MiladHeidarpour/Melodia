namespace Proj.Api.ViewModels.Admins.Users;

public class CreateUserVm
{
    public long RoleId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}