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

        public DbSet<HardwareInputType> HardwareInputTypes { get; set; }


        public DbSet<HardwareOutput> HardwareOutputs { get; set; }

        public DbSet<HardwareOutputSelector> HardwareOutputSelectors { get; set; }

        public DbSet<HardwareOutputType> HardwareOutputTypes { get; set; }


        public DbSet<HardwarePanelToken> HardwarePanelTokens { get; set; }

        public DbSet<HardwareBoard> HardwareBoards { get; set; }

        public DbSet<IOExtenderBus> IOExtenderBuses { get; set; }

        public DbSet<IOExtenderBit> IOExtenderBits { get; set; }

        public DbSet<HardwareComponent> HardwareComponents { get; set; }


        public DbSet<SimulatorEvent> SimulatorEvents { get; set; }


        // OnConfiguring removed - using service configuration from ServiceCollectionExtensions.AddDatabaseServices()
        // This allows proper use of the configured database path from appsettings.json
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>()
                .HasIndex(p => new { p.Id })
                .IsUnique(false);
            
            modelBuilder.Entity<AircraftModel>()
                .HasIndex(p => new { p.Id })
                .IsUnique(false);

            // Configure table names for entities that don't follow plural convention
            modelBuilder.Entity<HardwareInputType>()
                .ToTable("HardwareInputType");
                
            modelBuilder.Entity<HardwareOutputType>()
                .ToTable("HardwareOutputType");

            // Configure foreign key relationships for HardwareInput
            modelBuilder.Entity<HardwareInput>()
                .HasOne(i => i.HardwareInputType)
                .WithMany()
                .HasForeignKey(i => i.HardwareInputTypeId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<HardwareInput>()
                .HasOne(i => i.HardwarePanel)
                .WithMany(p => p.HardwareInput)
                .HasForeignKey(i => i.HardwarePanelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure HardwareInputSelector relationship with cascade delete
            modelBuilder.Entity<HardwareInputSelector>()
                .HasOne(s => s.HardwareInput)
                .WithMany(i => i.HardwareInputSelectorList)
                .HasForeignKey(s => s.HardwareInputId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure foreign key relationships for HardwareOutput
            modelBuilder.Entity<HardwareOutput>()
                .HasOne(o => o.HardwareOutputType)
                .WithMany()
                .HasForeignKey(o => o.HardwareOutputTypeId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<HardwareOutput>()
                .HasOne(o => o.HardwarePanel)
                .WithMany(p => p.HardwareOutput)
                .HasForeignKey(o => o.HardwarePanelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure HardwareOutputSelector relationship with cascade delete
            modelBuilder.Entity<HardwareOutputSelector>()
                .HasOne(s => s.HardwareOutput)
                .WithMany(o => o.HardwareOutputSelectorList)
                .HasForeignKey(s => s.HardwareOutputId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure enum conversions for SimulatorEvent
            modelBuilder.Entity<SimulatorEvent>()
                .Property(e => e.SimulatorEventType)
                .HasConversion<int>();
                
            modelBuilder.Entity<SimulatorEvent>()
                .Property(e => e.SimulatorSoftware)
                .HasConversion<int>();
                
            modelBuilder.Entity<SimulatorEvent>()
                .Property(e => e.SimulatorEventSdkType)
                .HasConversion<int>();

            // Configure enum conversions for HardwarePanel
            modelBuilder.Entity<HardwarePanel>()
                .Property(e => e.CockpitArea)
                .HasConversion<int>();
                
            modelBuilder.Entity<HardwarePanel>()
                .Property(e => e.HardwarePanelOwner)
                .HasConversion<int>();
        }
    }
}
