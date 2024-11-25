using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Domain.CategoryAgg.Repositories;
using Proj.Domain.CategoryAgg.Services;

namespace Proj.Application.Categories.Delete;

internal class DeleteCatgeoryCommandHandler : IBaseCommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;
    private readonly IFileService _fileService;

    public DeleteCatgeoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService, IFileService fileService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.CategoryId);

        if (category == null) 
            return OperationResult.NotFound("دسته بندی یافت نشد");

        var result = await _categoryRepository.DeleteCategory(request.CategoryId);

        if (result==true)
        {
            await _categoryRepository.Save();
            return OperationResult.Success("حذف دسته بندی با موفقیت انجام شد");
        }

        return OperationResult.Error("امکان حذف این دسته بندی وجود ندارد");
    }
}