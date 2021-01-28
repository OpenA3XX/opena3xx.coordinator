using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.DataContexts
{
    public class HardwareDataContext : DbContext
    {
        public HardwareDataContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<AircraftModel> AircraftModels { get; set; }
        
        public DbSet<HardwarePanel> HardwarePanels { get; set; }
        
        public DbSet<Manufacturer> Manufacturers { get; set; }
        
        public DbSet<HardwareInput> HardwareInputs { get; set; }
        
        public DbSet<HardwareOutput> HardwareOutputs { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Hardware));
    }
}