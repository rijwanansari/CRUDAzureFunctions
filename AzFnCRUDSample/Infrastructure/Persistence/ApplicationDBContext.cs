using AzFnCRUDSample.Domain;
using AzFnCRUDSample.Service.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AzFnCRUDSample.Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
