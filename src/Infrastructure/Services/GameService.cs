using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class GameService : BaseService<Game, long>, IGameService
{
    private readonly IGameRepository _entityRepository;
    
    public GameService(IGameRepository entityRepository) : base(entityRepository)
    {
        _entityRepository = entityRepository;
    }
    
    public override Task UpdateAsync(Game entity)
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