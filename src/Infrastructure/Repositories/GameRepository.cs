using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GameRepository : BaseRepository<Game, long>, IGameRepository
{
    public GameRepository(FragDbContext dbContext) : base(dbContext)
    {
    }
}