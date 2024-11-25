using Proj.Domain.CategoryAgg.Repositories;
using Proj.Domain.CategoryAgg.Services;

namespace Proj.Application.Categories.Services;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryDomainService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public bool IsSlugExist(string slug)
    {
        return _categoryRepository.Exists(s => s.Slug == slug);
    }
}