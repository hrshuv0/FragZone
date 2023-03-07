using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class CategoryService : BaseService<Category, long>, ICategoryService
{
    public CategoryService(ICategoryRepository entityRepository) : base(entityRepository)
    {
    }
}