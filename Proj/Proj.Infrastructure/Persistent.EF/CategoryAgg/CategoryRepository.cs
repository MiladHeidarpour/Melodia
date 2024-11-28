using Microsoft.EntityFrameworkCore;
using Proj.Domain.CategoryAgg;
using Proj.Domain.CategoryAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.CategoryAgg;

internal class CategoryRepository:BaseRepository<Category>,ICategoryRepository
{
    public CategoryRepository(ProjContext context) : base(context)
    {
    }

    public async Task<bool> DeleteCategory(long CategoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(f => f.Id == CategoryId);

        if (category == null)
            return false;

        //var isExistMusic = await _context.Musics.AnyAsync(f => f.Artists.Exists(f => f.Id == artistId));

        //if (isExistMusic)
        //    return false;

        _context.Remove(category);
        return true;
    }
}