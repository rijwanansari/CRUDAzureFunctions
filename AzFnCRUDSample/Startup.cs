
using AzFnCRUDSample.Infrastructure.Persistence;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using AzFnCRUDSample.Service.Menu;
using AzFnCRUDSample.Service.Common.Interface;
using AzFnCRUDSample.Service.Cart;

[assembly: FunctionsStartup(typeof(AzFnCRUDSample.Startup))]
namespace AzFnCRUDSample
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Register services
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("MyConnectionString"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IApplicationDBContext, ApplicationDBContext>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<ICartService, CartService>();
        }
    }
}
