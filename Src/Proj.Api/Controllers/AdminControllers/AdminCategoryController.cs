using Common.AspNetCore._Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj.Api.ViewModels.Admins.Categories;
using Proj.Application.Categories.Create;
using Proj.Application.Categories.Delete;
using Proj.Application.Categories.Edit;
using Proj.Presentation.Facade.Categories;
using Proj.Query.Categories.Dtos;

namespace Proj.Api.Controllers.AdminControllers;

public class AdminCategoryController : AdminApiController
{
    private readonly ICategoryFacade _facade;

    public AdminCategoryController(ICategoryFacade facade)
    {
        _facade = facade;
    }

    #region Query

    /// <summary>
    /// جستوجوی دسته بندی بر اساس شناسه
    /// </summary>
    /// <param name="categoryId">شناسه دسته بندی</param>
    /// <returns>دسته بندی</returns>
    [HttpGet("{categoryId}")]
    public async Task<ApiResult<CategoryDto?>> GetCategoryById(long categoryId)
    {
        var category = await _facade.GetCategoryById(categoryId);
        return QueryResult(category);
    }



    /// <summary>
    /// جستوجوی دسته بندی بر اساس شناسه اسلاگ
    /// </summary>
    /// <param name="slug">شناسه اسلاگ</param>
    /// <returns>دسته بندی</returns>
    [HttpGet("Slug/{slug}")]
    public async Task<ApiResult<CategoryDto?>> GetCategoryBySlug(string slug)
    {
        var category = await _facade.GetCategoryBySlug(slug);
        return QueryResult(category);
    }



    /// <summary>
    /// لیستی از تمام دسته بندی ها
    /// </summary>
    /// <returns>لیستی از تمام دسته بندی ها</returns>
    [HttpGet("Lists")]
    public async Task<ApiResult<List<CategoryDto>>> GetCategoryList()
    {
        var category = await _facade.GetCategoryList();
        return QueryResult(category);
    }
    #endregion

    #region Command

    /// <summary>
    /// ثبت دسته بندی
    /// </summary>
    /// <param name="command">اطلاعات دسته بندی</param>
    [HttpPost]
    public async Task<ApiResult> CreateCategory([FromForm] CreateCategoryVM command)
    {
        var result = await _facade.CreateCategory(new CreateCategoryCommand()
        {
            Title = command.Title,
            ImageName = command.ImageName,
            Slug = command.Slug,
            SeoData = command.SeoData.Map(),
        });
        return CommandResult(result);
    }



    /// <summary>
    /// ویرایش دسته بندی
    /// </summary>
    /// <param name="command">اطلاعات دسته بندی</param>
    [HttpPut]
    public async Task<ApiResult> EditCategory([FromForm] EditCategoryVM command)
    {
        var result = await _facade.EditCategory(new EditCategoryCommand()
        {
            CategoryId = command.CategoryId,
            Title = command.Title,
            ImageName = command.ImageName,
            Slug = command.Slug,
            SeoData = command.SeoData.Map(),
        });
        return CommandResult(result);
    }



    /// <summary>
    /// حذف دسته بندی
    /// </summary>
    /// <param name="categoryId">شناسه دسته بندی</param>
    [HttpDelete("{categoryId}")]
    public async Task<ApiResult> DeleteCategory(long categoryId)
    {
        var result = await _facade.DeleteCategory(new DeleteCategoryCommand(categoryId));
        return CommandResult(result);
    }
    #endregion
}