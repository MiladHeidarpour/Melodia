using Common.Application.FileUtil.Interfaces;
using Microsoft.EntityFrameworkCore;
using Proj.Application._Utilities;
using Proj.Domain.UserAgg;
using Proj.Domain.UserAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.UserAgg;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly IFileService _fileService;
    public UserRepository(ProjContext context, IFileService fileService) : base(context)
    {
        _fileService = fileService;
    }

    public async Task<bool> DeleteUser(long userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == userId);
        var oldAvatar = user.Avatar;
        if (user == null)
        {
            return false;
        }

        _context.Remove(user);
        DeleteOldAvatar(oldAvatar);
        return true;
    }
    private void DeleteOldAvatar(string oldAvatar)
    {
        if (oldAvatar == "Avatar.png")
        {
            return;
        }
        _fileService.DeleteFile(Directories.UserAvatars, oldAvatar);
    }
}