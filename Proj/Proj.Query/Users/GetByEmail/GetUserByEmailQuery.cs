using Common.Query;
using Proj.Query.Users.Dtos;

namespace Proj.Query.Users.GetByEmail;

public record GetUserByEmailQuery(string Email) : IQuery<UserDto?>;