using Common.Application;
using Proj.Application.Categories.Create;
using Proj.Application.Categories.Delete;
using Proj.Application.Categories.Edit;
using Proj.Query.Categories.Dtos;

namespace Proj.Presentation.Facade.Categories;

public interface ICategoryFacade
{
    //Command
    Task<OperationResult> CreateCategory(CreateCategoryCommand command);
    Task<OperationResult> EditCategory(EditCategoryCommand command);
    Task<OperationResult> DeleteCategory(DeleteCategoryCommand command);


    //Query
    Task<CategoryDto?> GetCategoryById(long categoryId);
    Task<CategoryDto?> GetCategoryBySlug(string slug);
    Task<List<CategoryDto>> GetCategoryList();
}