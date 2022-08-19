using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Sale.Persistence.Adapter.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContextFactory()
        {
        }

        //private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json")
        //    .Build();

        public AppDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            //builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            object value = builder.UseSqlServer(@"Data Source=.; Initial Catalog=SaleDb; Integrated Security=True");

            return new AppDbContext(builder.Options);
        }
    }
}
