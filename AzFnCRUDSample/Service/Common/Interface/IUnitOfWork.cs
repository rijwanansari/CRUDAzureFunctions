using System.Threading.Tasks;

namespace AzFnCRUDSample.Service.Common.Interface;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    Task<int> SaveAsync();
    int Save();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
