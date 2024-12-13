using Common.Query;
namespace Proj.Query.Users.DTOs;

public class UserFilterData : BaseDto
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Avatar { get; set; }
}
