using Common.Application;

namespace Proj.Application.Categories.Delete;

public record DeleteCategoryCommand(long CategoryId) : IBaseCommand;