using Common.Query;

namespace Proj.Query.Users.Dtos;

public class UserDto:BaseDto
{
    public long RoleId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Avatar { get; set; }
    public bool IsActive { get; set; }
}