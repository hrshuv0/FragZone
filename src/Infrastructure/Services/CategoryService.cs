using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class CategoryService : BaseService<Category, long>, ICategoryService
{
    #region CONFIG

    private readonly ICategoryRepository _entityRepository;

    public CategoryService(ICategoryRepository entityRepository) : base(entityRepository)
    {
        _entityRepository = entityRepository;
    }
    #endregion
    
    
    public override Task UpdateAsync(Category entity)
    {
        try
        {
            entity.UpdatedTime = DateTime.Now;
            return _entityRepository.UpdateAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}