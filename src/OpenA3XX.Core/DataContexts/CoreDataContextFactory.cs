using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OpenA3XX.Core.Configuration;

namespace OpenA3XX.Core.DataContexts
{
    public class CoreDataContextFactory : IDesignTimeDbContextFactory<CoreDataContext>
    {
        public CoreDataContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CoreDataContext>();
            dbContextOptionsBuilder.UseSqlite(CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Core));

            return new CoreDataContext(dbContextOptionsBuilder.Options);
        }
    }
}