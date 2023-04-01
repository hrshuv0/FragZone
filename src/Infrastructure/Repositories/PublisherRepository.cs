using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PublisherRepository : BaseRepository<Publisher, long>, IPublisherRepository
{
    public PublisherRepository(FragDbContext dbContext) : base(dbContext)
    {
    }
    
    
}