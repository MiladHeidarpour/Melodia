using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Proj.Application._Utilities;
using Proj.Domain.UserAgg.Repositories;
using Proj.Domain.UserAgg.Services;

namespace Proj.Application.Users.Edit;

internal class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;
    private readonly IFileService _fileService;

    public EditUserCommandHandler(IUserRepository repository, IUserDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);

        if (user==null)
        {
            return OperationResult.NotFound("کاربر مورد نظر یافت نشد");
        }
        var oldAvatar = user.Avatar;
        user.Edit(request.FullName, request.Email, request.PhoneNumber, _domainService);
        if (request.Avatar != null)
        {
            var avatar = await _fileService.SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
            user.SetAvatar(avatar);
        }
        DeleteOldAvatar(request.Avatar,oldAvatar);
        await _repository.Save();
        return OperationResult.Success("کاربر با موفقیت ویرایش شد");
    }
    private void DeleteOldAvatar(IFormFile? avatarFile, string oldAvatar)
    {
        if (avatarFile == null || oldAvatar == "Avatar.png")
        {
            return;
        }
        _fileService.DeleteFile(Directories.UserAvatars, oldAvatar);
    }
}