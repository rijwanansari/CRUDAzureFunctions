using AzFnCRUDSample.Domain.Common;
using AzFnCRUDSample.Service.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AzFnCRUDSample.Infrastructure.Persistence;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDBContext _context;
    private DbSet<T> _entities;

    public EfRepository(ApplicationDBContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
    {
        if (_context is DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

            foreach (var entry in entries)
            {
                try
                {
                    entry.State = EntityState.Unchanged;
                }
                catch (InvalidOperationException)
                {
                    // ignored
                }
            }
        }

        try
        {
            _context.SaveChanges();
            return exception.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    protected DbSet<T> Entities => _entities;

    public async Task<T> AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _entities.AddRangeAsync(entities);
        return entities;
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<T> UpdateAsync(object Id, T entity)
    {
        T exist = await _entities.FindAsync(Id);
        _context.Entry(exist).CurrentValues.SetValues(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(object id)
    {
        var entity = await _entities.FindAsync(id);
        if (entity == null)
            return false;

        _entities.Remove(entity);
        return true;
    }

    public bool DeleteRange(IEnumerable<T> entities)
    {
        if (entities == null)
            return false;

        _entities.RemoveRange(entities);
        return true;
    }

    public async Task<bool> Delete(T entity)
    {
        _entities.Remove(entity);
        return true;
    }

    public async Task<bool> Delete(object id)
    {
        var entity = await _entities.FindAsync(id);
        if (entity == null)
            return false;

        _entities.Remove(entity);
        return true;
    }

    public async Task<bool> Delete(Expression<Func<T, bool>> where)
    {
        var entities = _entities.Where(where);
        _entities.RemoveRange(entities);
        return true;
    }

    public async Task<T> Get(object id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task<T> Get(Expression<Func<T, bool>> where)
    {
        return await _entities.FirstOrDefaultAsync(where);
    }

    public IEnumerable<T> GetAll()
    {
        return _entities;
    }

    public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
    {
        return _entities.Where(where);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<int> Count()
    {
        return await _entities.CountAsync();
    }

    public async Task<int> Count(Expression<Func<T, bool>> where)
    {
        return await _entities.CountAsync(where);
    }

    public async Task<PaginatedList<T>> GetPaginatedResults(int pageNumber, int pageSize, Expression<Func<T, object>> orderBy,
        Expression<Func<T, bool>> where, bool descendingFiltered = true)
    {
        IQueryable<T> query = _entities.AsNoTracking().Where(where);
        if (descendingFiltered)
        {
            query = query.OrderByDescending(orderBy);
        }
        else
        {
            query = query.OrderBy(orderBy);
        }

        var count = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }

    public async Task<PaginatedList<T>> GetPaginatedResults(int pageNumber, int pageSize, Expression<Func<T, object>> orderBy,
        bool descendingFiltered = true)
    {
        IQueryable<T> query = _entities.AsNoTracking();
        if (descendingFiltered)
        {
            query = query.OrderByDescending(orderBy);
        }
        else
        {
            query = query.OrderBy(orderBy);
        }

        var count = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }

    public DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return _context.Set<TEntity>();
    }

    public IQueryable<T> Table => _entities;

    public IQueryable<T> TableNoTracking => _entities.AsNoTracking();
}
