using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OpenA3XX.Core.Configuration;

namespace OpenA3XX.Core.DataContexts
{
    public class HardwareDataContextFactory: IDesignTimeDbContextFactory<HardwareDataContext>
    {
        public HardwareDataContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<HardwareDataContext>();
            dbContextOptionsBuilder.UseSqlite(CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Hardware));

            return new HardwareDataContext(dbContextOptionsBuilder.Options);
        }
    }
}