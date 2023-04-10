using Core.Entities.Identity;
using Core.Entities.Photos;
using Core.Repositories;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository : IRepository
{
    #region CONFIG
    private readonly FragIdentityDbContext? _context;

    public Repository(FragIdentityDbContext? context)
    {
        _context = context;
    }

    #endregion
    
    public async Task Add<T>(T entity)
    {
        await _context!.AddAsync(entity!);
    }

    public async Task Delete<T>(T entity)
    {
        _context!.Remove(entity!);
    }

    public async Task<bool> SaveAll()
    {
        return await _context!.SaveChangesAsync() > 0;
    }

    public async Task<ApplicationUser> Get(string id)
    {
        var user = await _context!.Users
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user!;
    }

    public async Task<List<ApplicationUser>> Load(int pageNumber, int pageSize)
    {
        IQueryable<ApplicationUser> query = _context!.Users.AsQueryable();
        
        query = query
            .Include(u => u.Photos)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        var result = await query.ToListAsync();
        
        return result;
    }

    public async Task<Photo> GetPhoto(string id)
    {
        var photo = await _context!.Photos!.FirstOrDefaultAsync(p => p.Id.ToString() == id);

        return photo!;
    }

    public async Task<Photo> GetMainPhoto(string userId)
    {
        var photo =  await _context!.Photos!.Where(u => u.AppUserId == userId)
            .FirstOrDefaultAsync(p => p.IsMain);

        return photo!;
    }

    public async Task<Photo> GetMainPhotoForUser(string userId)
    {
        var photo = await _context!.Photos!.Where(u => u.AppUserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        
        return photo!;
    }
}