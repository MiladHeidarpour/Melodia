using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Users.Dtos;
using Proj.Query.Users.Mapper;

namespace Proj.Query.Users.GetByEmail;

public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserDto?>
{
    private readonly ProjContext _context;
    public GetUserByEmailQueryHandler(ProjContext context)
    {
        _context = context;
    }
    public async Task<UserDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == request.Email, cancellationToken);

        if (user == null)
        {
            return null;
        }

        return user.MapToDto();
    }
}