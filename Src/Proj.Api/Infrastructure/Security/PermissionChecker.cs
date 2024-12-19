using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Proj.Presentation.Facade.Roles;
using Proj.Presentation.Facade.Users;
using Proj.Domain.RoleAgg.Enums;
using Common.AspNetCore._Utils;

namespace Proj.Api.Infrastructure.Security;

public class PermissionChecker : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private IUserFacade _userFacade;
    private IRoleFacade _roleFacade;
    private readonly Permission _permission;

    public PermissionChecker(Permission permission)
    {
        _permission = permission;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (HasAllowAnonymous(context))
            return;

        _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();
        _roleFacade = context.HttpContext.RequestServices.GetRequiredService<IRoleFacade>();

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (await UserHasPermission(context) == false)
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult("Unauthorize");
        }
    }

    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
        bool hasAllowAnonymous = false;
        foreach (var f in metaData)
        {
            try
            {
                hasAllowAnonymous = f.TypeId.Name == "AllowAnonymousAttribute";
                if (hasAllowAnonymous)
                    break;
            }
            catch
            {
                // ignored
            }
        }

        return hasAllowAnonymous;
    }
    private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
    {
        var user = await _userFacade.GetUserById(context.HttpContext.User.GetUserId());
        if (user == null)
        {
            return false;
        }
        var roleId = user.RoleId;
        var roles = await _roleFacade.GetRoles();
        var userRole = roles.Where(r => roleId==r.Id);
        return userRole.Any(r => r.Permissions.Contains(_permission));
    }
}
