using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories.Hardware
{
    /// <summary>
    /// Repository for managing HardwareInput entities
    /// </summary>
    public class HardwareInputRepository : BaseRepository<HardwareInput>, IHardwareInputRepository
    {
        /// <summary>
        /// Initializes a new instance of the HardwareInputRepository
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        public HardwareInputRepository(DbContext context, ILogger<BaseRepository<HardwareInput>> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Gets all hardware inputs
        /// </summary>
        /// <returns>List of all hardware inputs</returns>
        public IList<HardwareInput> GetAllHardwareInputs()
        {
            Logger.LogInformation("Getting all hardware inputs");
            
            var result = GetAllIncluding(
                x => x.HardwareInputType,
                x => x.HardwarePanel,
                x => x.HardwareInputSelectorList)
                .ToList();
            
            Logger.LogInformation("Retrieved {Count} hardware inputs from database", result.Count);
            
            if (result.Count == 0)
            {
                Logger.LogWarning("No hardware inputs found in database - database might be empty or not seeded");
            }
            else
            {
                Logger.LogInformation("Hardware inputs found: {Names}", 
                    string.Join(", ", result.Select(x => $"'{x.Name}' (ID: {x.Id}, Panel: {x.HardwarePanelId})")));
            }
            
            return result;
        }

        /// <summary>
        /// Gets hardware inputs by panel ID
        /// </summary>
        /// <param name="panelId">The hardware panel ID</param>
        /// <returns>List of hardware inputs for the specified panel</returns>
        public IList<HardwareInput> GetHardwareInputsByPanelId(int panelId)
        {
            Logger.LogInformation("Getting hardware inputs for panel ID: {PanelId}", panelId);
            
            var result = FindBy(x => x.HardwarePanelId == panelId)
                .Include(x => x.HardwareInputType)
                .Include(x => x.HardwarePanel)
                .Include(x => x.HardwareInputSelectorList)
                .ToList();
            
            Logger.LogInformation("Retrieved {Count} hardware inputs for panel {PanelId}", result.Count, panelId);
            
            return result;
        }

        /// <summary>
        /// Gets a hardware input by its ID
        /// </summary>
        /// <param name="id">The hardware input ID</param>
        /// <returns>The hardware input or null if not found</returns>
        public HardwareInput GetHardwareInputBy(int id)
        {
            Logger.LogInformation("Getting hardware input by ID: {Id}", id);
            
            var result = Get(id);
            
            if (result != null)
            {
                Logger.LogInformation("Found hardware input: {Name} (ID: {Id}, Panel: {PanelId})", 
                    result.Name, result.Id, result.HardwarePanelId);
            }
            else
            {
                Logger.LogWarning("Hardware input with ID {Id} not found", id);
            }
            
            return result;
        }

        /// <summary>
        /// Adds a new hardware input
        /// </summary>
        /// <param name="hardwareInput">The hardware input to add</param>
        /// <returns>The added hardware input</returns>
        public HardwareInput AddHardwareInput(HardwareInput hardwareInput)
        {
            Logger.LogInformation("Adding new hardware input: {Name} for panel {PanelId}", 
                hardwareInput.Name, hardwareInput.HardwarePanelId);
            
            var result = Add(hardwareInput);
            Save(); // Explicit save since base repository no longer auto-saves
            
            Logger.LogInformation("Successfully added hardware input: {Name} (ID: {Id})", 
                result.Name, result.Id);
            
            return result;
        }

        /// <summary>
        /// Updates an existing hardware input
        /// </summary>
        /// <param name="hardwareInput">The hardware input to update</param>
        /// <returns>The updated hardware input</returns>
        public HardwareInput UpdateHardwareInput(HardwareInput hardwareInput)
        {
            Logger.LogInformation("Updating hardware input: {Name} (ID: {Id})", 
                hardwareInput.Name, hardwareInput.Id);
            
            var result = Update(hardwareInput, hardwareInput.Id);
            if (result != null)
            {
                Save(); // Explicit save since base repository no longer auto-saves
                Logger.LogInformation("Successfully updated hardware input: {Name} (ID: {Id})", 
                    result.Name, result.Id);
            }
            else
            {
                Logger.LogWarning("Failed to update hardware input with ID {Id} - not found", hardwareInput.Id);
            }
            
            return result;
        }

        /// <summary>
        /// Deletes a hardware input by its ID
        /// </summary>
        /// <param name="id">The hardware input ID to delete</param>
        public void DeleteHardwareInput(int id)
        {
            Logger.LogInformation("Deleting hardware input with ID: {Id}", id);
            
            var hardwareInput = Get(id);
            if (hardwareInput != null)
            {
                Delete(hardwareInput);
                Save(); // Explicit save since base repository no longer auto-saves
                Logger.LogInformation("Successfully deleted hardware input: {Name} (ID: {Id})", 
                    hardwareInput.Name, hardwareInput.Id);
            }
            else
            {
                Logger.LogWarning("Failed to delete hardware input with ID {Id} - not found", id);
                throw new EntityNotFoundException("HardwareInput", id);
            }
        }
    }
} 