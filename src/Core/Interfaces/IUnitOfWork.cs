using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    #region Repository

    ICategoryRepository CategoryRepository { get; }
    IPublisherRepository PublisherRepository { get; }

    #endregion

    #region Service

    ICategoryService CategoryService { get; }
    IPublisherService PublisherService { get; }

    #endregion

    void SaveChanges();
    Task SaveChangesAsync();
    public Task<IDbContextTransaction> BeginTransaction();
}