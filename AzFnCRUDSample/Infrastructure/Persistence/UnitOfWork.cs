using AzFnCRUDSample.Service.Common.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzFnCRUDSample.Infrastructure.Persistence;

internal class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDBContext _context;
    private IDbContextTransaction _dbContextTransaction;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var entityType = typeof(TEntity);

        if (!_repositories.ContainsKey(entityType))
        {
            var repositoryType = typeof(EfRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(entityType), _context);
            _repositories.Add(entityType, repositoryInstance);
        }

        return (IRepository<TEntity>)_repositories[entityType];
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public void BeginTransaction()
    {
        _dbContextTransaction = _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _dbContextTransaction?.Commit();
    }

    public void RollbackTransaction()
    {
        _dbContextTransaction?.Rollback();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContextTransaction?.Dispose();
            _context.Dispose();
        }
    }
}
