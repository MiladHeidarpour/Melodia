using Common.AspNetCore._Utils;
using Microsoft.AspNetCore.Mvc;
using Proj.Presentation.Facade.Categories;
using Proj.Query.Categories.Dtos;

namespace Proj.Api.Controllers;

public class CategoryController : ApiController
{
    private readonly ICategoryFacade _facade;
    public CategoryController(ICategoryFacade facade)
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
        var categories = await _facade.GetCategoryList();
        return QueryResult(categories);
    }

    #endregion
}