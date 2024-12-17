using Common.Domain.ValueObjects;

namespace Proj.Api.ViewModels.Admins;

public class SeoDataViewModel
{
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeyWords { get; set; }
    public bool IndexPage { get; set; } = false;
    public string? Canonical { get; set; }
    public string? Schema { get; set; }

    public SeoData Map()
    {
        return new SeoData(MetaKeyWords, MetaDescription, MetaTitle, IndexPage, Canonical, Schema);
    }
}