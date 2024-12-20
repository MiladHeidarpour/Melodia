﻿using Common.Query;
using Dapper;
using Proj.Infrastructure.Persistent.Dapper;
using Proj.Query.Users.Dtos.UserTokens;

namespace Proj.Query.Users.UserTokens.GetByJwtToken;

internal class GetUserTokenByJwtTokenQueryHandler : IQueryHandler<GetUserTokenByJwtTokenQuery, UserTokenDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserTokenByJwtTokenQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto?> Handle(GetUserTokenByJwtTokenQuery request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * From {_dapperContext.UserTokens} Where HashJwtToken=@hashJwtToken";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { hashJwtToken = request.HashJwtToken });
    }
}