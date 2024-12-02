using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.CategoryAgg.Repositories;
using Proj.Domain.CategoryAgg.Services;

namespace Proj.Application.Categories.Delete;

internal class DeleteCategoryCommandHandler : IBaseCommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _repository;
    private readonly ICategoryDomainService _domainService;
    private readonly IFileService _fileService;
    public DeleteCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetTracking(request.CategoryId);

        if (category == null)
            return OperationResult.NotFound("دسته بندی یافت نشد");

        var result = await _repository.DeleteCategory(request.CategoryId);

        if (result)
        {
            await _repository.Save();
            _fileService.DeleteFile(Directories.CategoryImages, category.ImageName);
            return OperationResult.Success("حذف دسته بندی با موفقیت انجام شد");
        }

        return OperationResult.Error("امکان حذف این دسته بندی وجود ندارد");
    }
}