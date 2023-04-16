using Core.Dtos.Identity;
using Core.Entities.Identity;
using Core.Entities.Photos;
using Core.Enums;
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

    public async Task<ApplicationUser> Update(string id, UserUpdateDto userDto)
    {
        try
        {
            var userExists = await _entityRepository.Get(id);
            if(userExists == null)
                throw new Exception("User not found");

            ApplicationUser user = new()
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                Email = userDto.Email,
                DisplayName = userDto.DisplayName,
                InGameName = userDto.InGameName,
                CreatedTime = userDto.CreatedTime,
                LastActive = userDto.LastActive,
            };
            user.Status = userDto.Status.ToLower() == "active"? StatusUser.Active: StatusUser.InActive;
            
            return await _entityRepository.Update(user);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> SaveAll()
    {
        return await _entityRepository.SaveAll();
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

    public Task<Photo> GetMainPhotoForUser(string userId)
    {
        return _entityRepository.GetMainPhotoForUser(userId);
    }
}