using Common.Domain.Repositories;

namespace Proj.Domain.UserAgg.Repositories;

public interface IUserRepository:IBaseRepository<User>
{
    Task<bool> DeleteUser(long userId);
}