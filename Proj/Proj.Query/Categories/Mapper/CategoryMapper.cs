using Proj.Domain.ArtistAgg;
using Proj.Domain.CategoryAgg;
using Proj.Query.Artists.Dtos;
using Proj.Query.Categories.Dtos;

namespace Proj.Query.Categories.Mapper;

internal static class CategoryMapper
{
    public static CategoryDto? Map(this Category? category)
    {
        if (category == null)
        {
            return null;
        }

        return new CategoryDto()
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            ImageName = category.ImageName,
            Slug = category.Slug,
            SeoData = category.SeoData,
        };
    }
    public static List<CategoryDto> Map(this List<Category> categories)
    {
        var model = new List<CategoryDto>();
        categories.ForEach(category =>
        {
            model.Add(new CategoryDto()
            {
                Id = category.Id,
                CreationDate = category.CreationDate,
                Title = category.Title,
                ImageName = category.ImageName,
                Slug = category.Slug,
                SeoData = category.SeoData,
            });
        });
        return model;
    }
}