using AzFnCRUDSample.Domain;
using AzFnCRUDSample.Service.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AzFnCRUDSample.Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        string connectionString = Environment.GetEnvironmentVariable("MyConnectionString");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}

        //model builder to remove pluralizing table names
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemType>().ToTable("ItemType");
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
