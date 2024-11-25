using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Proj.Application._Utilities;
using Proj.Domain.CategoryAgg;
using Proj.Domain.CategoryAgg.Repositories;
using Proj.Domain.CategoryAgg.Services;

namespace Proj.Application.Categories.Create;

internal class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;
    private readonly IFileService _fileService;
    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService, IFileService fileService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
        _fileService = fileService;
    }


    public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageName, Directories.CategoryImages);
        var category = new Category(request.Title, request.Slug, imageName, request.SeoData, _categoryDomainService);
        await _categoryRepository.AddAsync(category);
        return OperationResult.Success("دسته بندی با موفقیت ثبت شد");
    }
}