using Bakery.Sale.DomainApi.Services;
using Bakery.Sale.Persistence.Adapter.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bakery.Sale.Persistence.Adapter
{
    public static class PersistenceExtensions
    {
        public static void AddPersistence(this IServiceCollection serviceCollection, AppSettings appSettings)
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(appSettings.SqlAzureInventory.ConnectionString));
        }
    }
}
