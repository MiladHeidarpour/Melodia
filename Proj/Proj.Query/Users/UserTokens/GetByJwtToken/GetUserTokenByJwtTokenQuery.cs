using Common.Query;
using Proj.Query.Users.Dtos.UserTokens;

namespace Proj.Query.Users.UserTokens.GetByJwtToken;

public record GetUserTokenByJwtTokenQuery(string HashJwtToken) : IQuery<UserTokenDto?>;