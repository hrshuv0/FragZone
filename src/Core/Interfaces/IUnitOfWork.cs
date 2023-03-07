using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    
    
    
    
    
    
    ICategoryService CategoryService { get; }
    
    void SaveChanges();
    Task SaveChangesAsync();
    public Task<IDbContextTransaction> BeginTransaction();
}