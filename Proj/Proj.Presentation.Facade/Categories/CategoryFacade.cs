using Common.Application;
using MediatR;
using Proj.Application.Categories.Create;
using Proj.Application.Categories.Delete;
using Proj.Application.Categories.Edit;
using Proj.Query.Categories.Dtos;
using Proj.Query.Categories.GetById;
using Proj.Query.Categories.GetBySlug;
using Proj.Query.Categories.GetList;

namespace Proj.Presentation.Facade.Categories;

internal class CategoryFacade : ICategoryFacade
{
    private readonly IMediator _mediator;

    public CategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditCategory(EditCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteCategory(DeleteCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CategoryDto?> GetCategoryById(long categoryId)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(categoryId));
    }

    public async Task<CategoryDto?> GetCategoryBySlug(string slug)
    {
        return await _mediator.Send(new GetCategoryBySlugQuery(slug));
    }

    public async Task<List<CategoryDto>> GetCategoryList()
    {
        return await _mediator.Send(new GetCategoryListQuery());
    }
}