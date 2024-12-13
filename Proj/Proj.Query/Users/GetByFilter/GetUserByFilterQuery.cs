using Common.Query;
using Proj.Query.Users.DTOs;

namespace Proj.Query.Users.GetByFilter;

public class GetUserByFilterQuery:QueryFilter<UserFilterResult,UserFilterParams>
{
    public GetUserByFilterQuery(UserFilterParams filterParams) : base(filterParams)
    {
    }
}