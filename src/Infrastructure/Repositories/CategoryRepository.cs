using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category, long>, ICategoryRepository
{
    public CategoryRepository(FragDbContext dbContext) : base(dbContext)
    {
    }
    
    
}