﻿using Common.Query;
using Dapper;
using Proj.Infrastructure.Persistent.Dapper;
using Proj.Query.Users.Dtos.UserTokens;

namespace Proj.Query.Users.UserTokens.GetByRefreshToken;

internal class GetUserTokenByRefreshTokenQueryHandler : IQueryHandler<GetUserTokenByRefreshTokenQuery, UserTokenDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserTokenByRefreshTokenQueryHandler(DapperContext dappercontext)
    {
        _dapperContext = dappercontext;
    }

    public async Task<UserTokenDto?> Handle(GetUserTokenByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {_dapperContext.UserTokens} Where HashRefreshToken=@hashRefreshToken";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { HashRefreshToken = request.HashRefreshToken });
    }
}