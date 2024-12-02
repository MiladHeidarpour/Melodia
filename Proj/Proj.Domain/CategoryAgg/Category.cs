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
    public Category(string title, string imageName, string slug, SeoData seoData, ICategoryDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        Guard(title, slug, domainService);
        Title = title;
        ImageName = imageName;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }
    public void Edit(string title, string slug, SeoData seoData, ICategoryDomainService domainService)
    {
        Guard(title, slug, domainService);
        Title = title;
        Slug = slug?.ToSlug();
        SeoData = seoData;
    }
    public void SetCategoryImg(string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        ImageName = imageName;
    }
    private void Guard(string title, string slug, ICategoryDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        if (slug != Slug)
        {
            if (domainService.IsSlugExist(slug))
            {
                throw new SlugIsDuplicateException();
            }
        }
    }
}