using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OpenA3XX.Coordinator.TestHarness
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