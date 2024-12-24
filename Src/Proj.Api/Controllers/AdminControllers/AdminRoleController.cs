using Common.AspNetCore._Utils;
using Microsoft.AspNetCore.Mvc;
using Proj.Api.ViewModels.Admins.Roles;
using Proj.Application.Roles.Create;
using Proj.Application.Roles.Delete;
using Proj.Application.Roles.Edit;
using Proj.Presentation.Facade.Roles;
using Proj.Query.Roles.Dtos;

namespace Proj.Api.Controllers.AdminControllers;

public class AdminRoleController : AdminApiController
{
    private readonly IRoleFacade _facade;

    public AdminRoleController(IRoleFacade facade)
    {
        _facade = facade;
    }

    #region Query
    /// <summary>
    /// جستوجوی نقش بر اساس شناسه نقش
    /// </summary>
    /// <param name="roleId">شناسه نقش</param>
    /// <returns>نقش</returns>
    [HttpGet("{roleId}")]
    public async Task<ApiResult<RoleDto?>> GetRoleById(long roleId)
    {
        var result = await _facade.GetRoleById(roleId);
        return QueryResult(result);
    }



    /// <summary>
    /// لیستی از تمام نقش ها
    /// </summary>
    /// <returns>لیستی از تمام نقش ها</returns>
    [HttpGet("List")]
    public async Task<ApiResult<List<RoleDto>>> GetRoles()
    {
        var result = await _facade.GetRoles();
        return QueryResult(result);
    }

    #endregion

    #region Command

    /// <summary>
    /// ثبت نقش
    /// </summary>
    /// <param name="command">اطلاعات نقش</param>
    [HttpPost]
    public async Task<ApiResult> CreateRole(CreateRoleVM command)
    {
        var result = await _facade.CreateRole(new CreateRoleCommand(command.Title, command.Permissions));
        return CommandResult(result);
    }



    /// <summary>
    /// ویرایش نقش
    /// </summary>
    /// <param name="command">اطلاعات نقش</param>
    [HttpPut]
    public async Task<ApiResult> EditRole(EditRoleVM command)
    {
        var result = await _facade.EditRole(new EditRoleCommand(command.RoleId, command.Title, command.Permissions));
        return CommandResult(result);
    }



    /// <summary>
    /// حذف نقش
    /// </summary>
    /// <param name="roleId">شناسه نقش</param>
    [HttpDelete("{roleId}")]
    public async Task<ApiResult> DeleteRole(long roleId)
    {
        var result = await _facade.DeleteRole(new DeleteRoleCommand(roleId));
        return CommandResult(result);
    }

    #endregion
}