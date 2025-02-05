﻿using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.EntityFrameworkCore;
using Proj.Application._Utilities;
using Proj.Domain.ArtistAgg;
using Proj.Domain.CategoryAgg;
using Proj.Domain.CategoryAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.CategoryAgg;

internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly IFileService _fileService;
    public CategoryRepository(ProjContext context, IFileService fileService) : base(context)
    {
        _fileService = fileService;
    }

    public async Task<bool> DeleteCategory(long categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(f => f.Id == categoryId);

        if (category == null)
            return false;

        var isExistMusic = await _context.MusicAlbums.AnyAsync(f => f.CategoryId == categoryId);

        if (isExistMusic)
            return false;

        _context.Remove(category);
        _fileService.DeleteFile(Directories.CategoryImages, category.ImageName);
        return true;
    }
}