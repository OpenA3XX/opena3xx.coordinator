using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.DataContexts
{
    public class CoreDataContext : DbContext
    {
        public CoreDataContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<AircraftModel> AircraftModels { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<HardwarePanel> HardwarePanels { get; set; }
        
        
        public DbSet<HardwareInput> HardwareInputs { get; set; }
        
        public DbSet<HardwareInputSelector> HardwareInputSelectors { get; set; }
        
        
        public DbSet<HardwareOutput> HardwareOutputs { get; set; }
        
        public DbSet<HardwareOutputSelector> HardwareOutputSelectors { get; set; }

        

        public DbSet<HardwarePanelToken> HardwarePanelTokens { get; set; }

        public DbSet<SystemConfiguration> SystemConfiguration { get; set; }

        
        
        public DbSet<HardwareBoard> HardwareBoards { get; set; }

        public DbSet<IOExtenderBus> IOExtenderBuses { get; set; }

        public DbSet<IOExtenderBit> IOExtenderBits { get; set; }
        
        
        public DbSet<SimulatorEvent> SimulatorEvents { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Core));
        }
    }
}