using Bakery.Sale.Persistence.Adapter.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bakery.Sale.Persistence.Adapter
{
    public static class PersistenceExtensions
    {
        public static void AddPersistence(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=tcp:bakery0.database.windows.net,1433;Initial Catalog=SaleDb;Persist Security Info=False;User ID=bakery;Password=dojonet02.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
        }
    }
}
