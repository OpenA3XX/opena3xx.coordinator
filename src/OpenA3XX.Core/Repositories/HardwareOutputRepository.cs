using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    /// <summary>
    /// Repository for managing HardwareOutput entities
    /// </summary>
    public class HardwareOutputRepository : BaseRepository<HardwareOutput>, IHardwareOutputRepository
    {
        /// <summary>
        /// Initializes a new instance of the HardwareOutputRepository
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        public HardwareOutputRepository(DbContext context, ILogger<BaseRepository<HardwareOutput>> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Gets all hardware outputs with their related entities
        /// </summary>
        /// <returns>List of all hardware outputs with includes</returns>
        public IList<HardwareOutput> GetAllHardwareOutputs()
        {
            Logger.LogInformation("Getting all hardware outputs");
            
            var result = GetAll()
                .Include(o => o.HardwareOutputType)
                .Include(o => o.HardwarePanel)
                .Include(o => o.HardwareOutputSelectorList)
                .ToList();
            
            Logger.LogInformation("Retrieved {Count} hardware outputs from database", result.Count);
            
            return result;
        }

        /// <summary>
        /// Gets hardware outputs by panel ID
        /// </summary>
        /// <param name="panelId">The panel ID to filter by</param>
        /// <returns>List of hardware outputs for the specified panel</returns>
        public IList<HardwareOutput> GetHardwareOutputsByPanelId(int panelId)
        {
            Logger.LogInformation("Getting hardware outputs for panel ID: {PanelId}", panelId);
            
            var result = FindBy(output => output.HardwarePanelId == panelId)
                .Include(o => o.HardwareOutputType)
                .Include(o => o.HardwarePanel)
                .Include(o => o.HardwareOutputSelectorList)
                .ToList();
            
            Logger.LogInformation("Retrieved {Count} hardware outputs for panel {PanelId}", result.Count, panelId);
            
            return result;
        }

        /// <summary>
        /// Gets a specific hardware output by ID
        /// </summary>
        /// <param name="id">The hardware output ID</param>
        /// <returns>The hardware output or null if not found</returns>
        public HardwareOutput GetHardwareOutputBy(int id)
        {
            Logger.LogInformation("Getting hardware output by ID: {HardwareOutputId}", id);
            
            var result = FindBy(output => output.Id == id)
                .Include(o => o.HardwareOutputType)
                .Include(o => o.HardwarePanel)
                .Include(o => o.HardwareOutputSelectorList)
                .FirstOrDefault();
                
            if (result == null)
            {
                Logger.LogWarning("Hardware output with ID {HardwareOutputId} not found", id);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved hardware output with ID {HardwareOutputId}", id);
            }
            
            return result;
        }

        /// <summary>
        /// Adds a new hardware output
        /// </summary>
        /// <param name="hardwareOutput">The hardware output to add</param>
        /// <returns>The added hardware output</returns>
        public HardwareOutput AddHardwareOutput(HardwareOutput hardwareOutput)
        {
            Logger.LogInformation("Adding new hardware output: {Name} for panel {PanelId}", 
                hardwareOutput.Name, hardwareOutput.HardwarePanelId);
            
            var result = Add(hardwareOutput);
            Save(); // Explicit save since base repository no longer auto-saves
            
            Logger.LogInformation("Successfully added hardware output: {Name} (ID: {Id}) for panel {PanelId}", 
                result.Name, result.Id, result.HardwarePanelId);
            
            return result;
        }

        /// <summary>
        /// Updates an existing hardware output
        /// </summary>
        /// <param name="hardwareOutput">The hardware output to update</param>
        /// <returns>The updated hardware output</returns>
        public HardwareOutput UpdateHardwareOutput(HardwareOutput hardwareOutput)
        {
            Logger.LogInformation("Updating hardware output: {Name} (ID: {Id}) for panel {PanelId}", 
                hardwareOutput.Name, hardwareOutput.Id, hardwareOutput.HardwarePanelId);
            
            var result = Update(hardwareOutput, hardwareOutput.Id);
            if (result != null)
            {
                Save(); // Explicit save since base repository no longer auto-saves
                Logger.LogInformation("Successfully updated hardware output: {Name} (ID: {Id}) for panel {PanelId}", 
                    result.Name, result.Id, result.HardwarePanelId);
            }
            
            return result;
        }

        /// <summary>
        /// Deletes a hardware output by its ID
        /// </summary>
        /// <param name="id">The hardware output ID to delete</param>
        public void DeleteHardwareOutput(int id)
        {
            Logger.LogInformation("Deleting hardware output with ID: {Id}", id);
            
            var hardwareOutput = Get(id);
            if (hardwareOutput != null)
            {
                // Note: With the cascade delete configuration in CoreDataContext,
                // related HardwareOutputSelector records will be automatically deleted
                
                Delete(hardwareOutput);
                Save(); // Explicit save since base repository no longer auto-saves
                
                Logger.LogInformation("Successfully deleted hardware output: {Name} (ID: {Id})", 
                    hardwareOutput.Name, hardwareOutput.Id);
            }
            else
            {
                Logger.LogWarning("Failed to delete hardware output with ID {Id} - not found", id);
                throw new EntityNotFoundException("HardwareOutput", id);
            }
        }
    }
} 