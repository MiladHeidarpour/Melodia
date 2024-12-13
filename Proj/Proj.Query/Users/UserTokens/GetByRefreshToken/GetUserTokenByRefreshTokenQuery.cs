using Common.Query;
using Proj.Query.Users.Dtos.UserTokens;

namespace Proj.Query.Users.UserTokens.GetByRefreshToken;

public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IQuery<UserTokenDto?>;