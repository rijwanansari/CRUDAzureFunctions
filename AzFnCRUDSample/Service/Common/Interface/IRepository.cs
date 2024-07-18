using AzFnCRUDSample.Domain.Common;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AzFnCRUDSample.Service.Common.Interface;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    Task<T> UpdateAsync(object Id, T entity);
    Task<bool> DeleteAsync(object id);
    bool DeleteRange(IEnumerable<T> entities);
    Task<bool> Delete(T entity);
    Task<bool> Delete(object id);
    Task<bool> Delete(Expression<Func<T, bool>> where);
    Task<T> Get(object id);
    Task<T> Get(Expression<Func<T, bool>> where);
    IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    IEnumerable<T> GetAll();
    Task<int> Count(Expression<Func<T, bool>> where);
    Task<int> Count();
    Task<PaginatedList<T>> GetPaginatedResults(int pageNumber, int pageSize,
        Expression<Func<T, object>> orderBy, Expression<Func<T, bool>> where, bool descendingFiltered = true);
    Task<PaginatedList<T>> GetPaginatedResults(int pageNumber, int pageSize,
        Expression<Func<T, object>> orderBy, bool descendingFiltered = true);

    #region Properties

    /// <summary>
    /// Gets a table
    /// </summary>
    IQueryable<T> Table { get; }

    /// <summary>
    /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
    /// </summary>
    IQueryable<T> TableNoTracking { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    #endregion
}
