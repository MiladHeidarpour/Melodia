using Common.Domain.Base;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Proj.Domain.CategoryAgg.Services;

namespace Proj.Domain.CategoryAgg;

public class Category : AggregateRoot
{
    public string Title { get; private set; }
    public string ImageName { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }

    private Category()
    {

    }

    public Category(string title, string slug, string imageName, SeoData seoData, ICategoryDomainService categoryService)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        Gaurd(title, slug, categoryService);
        Title = title;
        ImageName = imageName;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }

    public void Edit(string title, string slug, SeoData seoData, ICategoryDomainService categoryService)
    {
        Gaurd(title, slug, categoryService);
        Title = title;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }

    public void SetCategoryImg(string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        ImageName = imageName;
    }

    public void Gaurd(string title, string slug, ICategoryDomainService categoryService)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        if (slug != Slug)
        {
            if (categoryService.IsSlugExist(slug) == true)
            {
                throw new SlugIsDuplicateException();
            }
        }
    }
}