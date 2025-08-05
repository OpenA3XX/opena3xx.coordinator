using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Services.Audit;

namespace OpenA3XX.Core.DataContexts
{
    public class CoreDataContext : DbContext
    {
        private readonly ILogger<CoreDataContext> _logger;

        public CoreDataContext(DbContextOptions options)
            : base(options)
        {
        }

        public CoreDataContext(DbContextOptions options, ILogger<CoreDataContext> logger)
            : base(options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

        public DbSet<AuditEntry> AuditEntries { get; set; }

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

            // Configure HardwareBoard cascade delete relationships
            modelBuilder.Entity<IOExtenderBus>()
                .HasOne(b => b.HardwareBoard)
                .WithMany(hb => hb.Buses)
                .HasForeignKey(b => b.HardwareBoardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure IOExtenderBit cascade delete relationships
            modelBuilder.Entity<IOExtenderBit>()
                .HasOne(bit => bit.IOExtenderBus)
                .WithMany(bus => bus.Bits)
                .HasForeignKey(bit => bit.IOExtenderBusId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <summary>
        /// Override SaveChanges to automatically create audit entries
        /// </summary>
        public override int SaveChanges()
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = base.SaveChanges();
            
            // Save audit entries in a separate context to avoid recursion
            if (auditEntries.Count > 0)
            {
                SaveAuditEntries(auditEntries);
            }
            
            return result;
        }

        /// <summary>
        /// Override SaveChangesAsync to automatically create audit entries
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            
            // Save audit entries in a separate context to avoid recursion
            if (auditEntries.Count > 0)
            {
                await SaveAuditEntriesAsync(auditEntries);
            }
            
            return result;
        }

        /// <summary>
        /// Creates audit entries for all changes before saving
        /// </summary>
        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Skip audit entries themselves to avoid infinite recursion
                if (entry.Entity is AuditEntry)
                    continue;

                // Skip unchanged entries
                if (entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry
                {
                    EntityName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    EntityId = GetEntityId(entry),
                    Timestamp = DateTime.UtcNow,
                    OldValues = entry.State == EntityState.Modified ? GetOldValues(entry) : null,
                    NewValues = entry.State != EntityState.Deleted ? GetNewValues(entry) : null
                };

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }

        /// <summary>
        /// Processes audit entries after saving changes
        /// </summary>
        private void OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return;

            // Add audit entries to the context for the next save
            AuditEntries.AddRange(auditEntries);
        }

        /// <summary>
        /// Processes audit entries after saving changes (async)
        /// </summary>
        private void OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return;

            // Add audit entries to the context for the next save
            AuditEntries.AddRange(auditEntries);
            
            // Note: We don't call SaveChangesAsync here to avoid infinite recursion
            // The audit entries will be saved in the next SaveChanges call
        }

        /// <summary>
        /// Saves audit entries using a separate context to avoid recursion
        /// </summary>
        private void SaveAuditEntries(List<AuditEntry> auditEntries)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<CoreDataContext>();
                optionsBuilder.UseSqlite(Database.GetConnectionString());
                
                using var auditContext = new CoreDataContext(optionsBuilder.Options);
                auditContext.AuditEntries.AddRange(auditEntries);
                auditContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to save audit entries");
            }
        }

        /// <summary>
        /// Saves audit entries using a separate context to avoid recursion (async)
        /// </summary>
        private async Task SaveAuditEntriesAsync(List<AuditEntry> auditEntries)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<CoreDataContext>();
                optionsBuilder.UseSqlite(Database.GetConnectionString());
                
                using var auditContext = new CoreDataContext(optionsBuilder.Options);
                auditContext.AuditEntries.AddRange(auditEntries);
                await auditContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to save audit entries");
            }
        }

        /// <summary>
        /// Gets the entity ID from the entry
        /// </summary>
        private int GetEntityId(EntityEntry entry)
        {
            var keyName = entry.Metadata.FindPrimaryKey()?.Properties
                .Select(p => p.Name)
                .SingleOrDefault();

            if (keyName != null)
            {
                var keyValue = entry.Property(keyName).CurrentValue;
                if (keyValue is int intValue)
                    return intValue;
            }

            return 0;
        }

        /// <summary>
        /// Gets the old values for modified entities
        /// </summary>
        private string GetOldValues(EntityEntry entry)
        {
            var oldValues = new Dictionary<string, object>();
            foreach (var property in entry.Properties)
            {
                if (property.IsModified)
                {
                    oldValues[property.Metadata.Name] = property.OriginalValue;
                }
            }
            return JsonSerializer.Serialize(oldValues);
        }

        /// <summary>
        /// Gets the new values for entities
        /// </summary>
        private string GetNewValues(EntityEntry entry)
        {
            var newValues = new Dictionary<string, object>();
            foreach (var property in entry.Properties)
            {
                if (property.CurrentValue != null)
                {
                    newValues[property.Metadata.Name] = property.CurrentValue;
                }
            }
            return JsonSerializer.Serialize(newValues);
        }
    }
}
