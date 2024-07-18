using AzFnCRUDSample.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AzFnCRUDSample.Service.Common.Interface
{
    public interface IApplicationDBContext
    {
        DbSet<ItemType> ItemTypes { get; set; }
        DbSet<ItemView> ItemViews { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
