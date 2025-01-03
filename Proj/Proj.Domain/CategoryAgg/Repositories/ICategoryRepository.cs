﻿using Common.Domain.Repositories;

namespace Proj.Domain.CategoryAgg.Repositories;

public interface ICategoryRepository:IBaseRepository<Category>
{
    Task<bool> DeleteCategory(long categoryId);
}