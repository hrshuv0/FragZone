using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class PublisherService : BaseService<Publisher, long>, IPublisherService
{
    #region CONFIG

    private readonly IPublisherRepository _entityRepository;

    public PublisherService(IPublisherRepository entityRepository) : base(entityRepository)
    {
        _entityRepository = entityRepository;
    }
    #endregion
    
    
    public override Task UpdateAsync(Publisher entity)
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