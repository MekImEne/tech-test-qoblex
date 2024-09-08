using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BundleTree.Data;

namespace BundleTree.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<BundleContext>
    {
        public BundleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BundleContext>();
            optionsBuilder.UseSqlite("Data Source=bundles.db");

            return new BundleContext(optionsBuilder.Options);
        }
    }
}
