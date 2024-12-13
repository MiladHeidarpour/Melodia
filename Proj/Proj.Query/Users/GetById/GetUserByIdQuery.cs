using Common.Query;
using Proj.Query.Users.Dtos;

namespace Proj.Query.Users.GetById;

public record GetUserByIdQuery(long UserId) : IQuery<UserDto?>;