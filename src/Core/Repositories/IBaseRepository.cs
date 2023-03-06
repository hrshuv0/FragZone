using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Repositories;

public interface IBaseRepository<TEntity, TKey> where TEntity : IBaseEntity<TKey>
{
    #region LINQ Async

    Task<IList<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true);

    (IList<TResult> Items, int Total, int TotalFilter) Get<TResult>(Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? predicate= null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageIndex = 1, int pageSize = 10,
        bool disableTracking = true);

    TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate=null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, 
        bool disableTracking = true);

    TEntity GetById(TKey id);
    int GetCount(Expression<Func<TEntity, bool>>? predicate = null);
    bool IsExists(Expression<Func<TEntity, bool>>? predicate);


    #endregion

    #region LINQ


    #endregion

    #region SQL



    #endregion
}