using Common.Query;
using Proj.Query.Users.Dtos;

namespace Proj.Query.Users.GetByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IQuery<UserDto?>;
