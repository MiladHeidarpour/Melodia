using Common.Query.Filter;
namespace Proj.Query.Users.DTOs;

public class UserFilterParams : BaseFilterParam
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public long? Id { get; set; }
}
