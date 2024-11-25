using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Proj.Application._Utilities;
using Proj.Domain.CategoryAgg.Repositories;
using Proj.Domain.CategoryAgg.Services;

namespace Proj.Application.Categories.Edit;

internal class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;
    private readonly IFileService _fileService;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService, IFileService fileService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.CategoryId);

        if (category == null)
            return OperationResult.NotFound("دسته بندی مورد نظر یافت نشد");

        category.Edit(request.Title, request.Slug, request.SeoData, _categoryDomainService);

        var oldImage = category.ImageName;
        if (request.ImageName != null)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.ImageName, Directories.CategoryImages);
            category.SetCategoryImg(imageName);
        }

        await _categoryRepository.Save();
        RemoveOldImage(request.ImageName,oldImage);
        return OperationResult.Success("دسته بندی با موفقیت ثبت شد");
    }

    private void RemoveOldImage(IFormFile imageFile, string oldImageName)
    {
        if (imageFile != null)
        {
            _fileService.DeleteFile(Directories.CategoryImages, oldImageName);
        }
    }
}