using Proj.Domain.UserAgg;
using Proj.Domain.UserAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.UserAgg;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ProjContext context) : base(context)
    {
    }
}