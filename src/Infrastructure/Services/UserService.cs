using Core.Entities.Identity;
using Core.Entities.Photos;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    #region CONFIG
    private readonly IRepository _entityRepository;

    public UserService(IRepository entityRepository)
    {
        _entityRepository = entityRepository;
    }

    #endregion
    
    public Task Add<T>(T entity)
    {
        try
        {
            return _entityRepository.Add(entity);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task Delete<T>(T entity)
    {
        return _entityRepository.Delete(entity);
    }

    public Task<bool> SaveAll()
    {
        return _entityRepository.SaveAll();
    }

    public Task<ApplicationUser> Get(string id)
    {
        return _entityRepository.Get(id);
    }

    public Task<List<ApplicationUser>> Load(int pageNumber, int pageSize)
    {
        return _entityRepository.Load(pageNumber, pageSize);
    }

    public Task<Photo> GetPhoto(string id)
    {
        return _entityRepository.GetPhoto(id);
    }

    public Task<Photo> GetMainPhoto(string userId)
    {
        return _entityRepository.GetMainPhoto(userId);
    }
}