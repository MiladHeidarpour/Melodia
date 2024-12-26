using Common.Query;
using Proj.Query.Users.Dtos;

namespace Proj.Query.Users.GetList;

public record GetUserListQuery : IQuery<List<UserDto?>>;