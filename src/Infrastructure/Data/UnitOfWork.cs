using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data;

public  class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly FragDbContext _dbContext;
    private readonly FragIdentityDbContext _identityDbContext;

    #region Repository

    public ICategoryRepository CategoryRepository { get; }
    public IPublisherRepository PublisherRepository { get; }
    public IGameRepository GameRepository { get; }
    public IRepository Repository { get; }

    #endregion

    #region Service

    public ICategoryService CategoryService { get; }
    public IPublisherService PublisherService { get; }
    public IGameService GameService { get; }
    public IUserService UserService { get; }

    #endregion

    public UnitOfWork(FragDbContext dbContext, FragIdentityDbContext identityDbContext)
    {
        _dbContext = dbContext;
        _identityDbContext = identityDbContext;

        #region Repo
        CategoryRepository = new CategoryRepository(dbContext);
        PublisherRepository = new PublisherRepository(dbContext);
        GameRepository = new GameRepository(dbContext);
        Repository = new Repository(_identityDbContext);
        #endregion

        CategoryService = new CategoryService(CategoryRepository);
        PublisherService = new PublisherService(PublisherRepository);
        GameService = new GameService(GameRepository);
        UserService = new UserService(Repository);
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

    public async Task<bool> SaveAllAsync()
    {
        return await _identityDbContext.SaveChangesAsync() > 0;
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