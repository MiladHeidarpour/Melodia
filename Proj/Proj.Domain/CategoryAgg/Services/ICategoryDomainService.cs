﻿namespace Proj.Domain.CategoryAgg.Services;

public interface ICategoryDomainService
{
    bool IsSlugExist(string slug);
}