using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data;

public  class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly FragDbContext _dbContext;

    public ICategoryRepository CategoryRepository { get; }
    public ICategoryService CategoryService { get; }
    
    

    public UnitOfWork(FragDbContext dbContext)
    {
        _dbContext = dbContext;
        
        CategoryRepository = new CategoryRepository(dbContext);
        CategoryService = new CategoryService(CategoryRepository);
    }

    #region Helper

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransaction()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

    #endregion

    #region Dispose

    ~UnitOfWork()
    {
        Dispose(false);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if(!_disposed && disposing)
            _dbContext.Dispose();
        _disposed = true;
    }
    #endregion
    
}