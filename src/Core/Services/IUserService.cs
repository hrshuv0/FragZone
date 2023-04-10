using Core.Entities.Identity;
using Core.Entities.Photos;

namespace Core.Services;

public interface IUserService
{
    Task Add<T>(T entity);
    Task Delete<T>(T entity);
    Task<bool> SaveAll();
    Task<ApplicationUser?> Get(string id);
    Task<List<ApplicationUser>> Load(int pageNumber, int pageSize);

    Task<Photo?> GetPhoto(string id);
    Task<Photo> GetMainPhoto(string userId);
    Task<Photo> GetMainPhotoForUser(string userId);
}