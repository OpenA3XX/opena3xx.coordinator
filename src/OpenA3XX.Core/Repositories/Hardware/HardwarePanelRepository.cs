using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories.Hardware
{
    /// <summary>
    /// Repository for managing HardwarePanel entities
    /// </summary>
    public class HardwarePanelRepository : BaseRepository<HardwarePanel>, IHardwarePanelRepository
    {
        /// <summary>
        /// Initializes a new instance of the HardwarePanelRepository
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        public HardwarePanelRepository(DbContext context, ILogger<BaseRepository<HardwarePanel>> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Adds a new hardware panel
        /// </summary>
        /// <param name="hardwarePanel">The hardware panel to add</param>
        /// <returns>The added hardware panel</returns>
        public HardwarePanel AddHardwarePanel(HardwarePanel hardwarePanel)
        {
            var result = Add(hardwarePanel);
            Save();
            return result;
        }

        /// <summary>
        /// Gets all hardware panels with basic information (aircraft model and manufacturer)
        /// </summary>
        /// <returns>List of all hardware panels with basic includes</returns>
        public IList<HardwarePanel> GetAllHardwarePanels()
        {
            return GetAll().IncludeBasic().ToList();
        }

        /// <summary>
        /// Gets detailed information for a specific hardware panel including all related entities
        /// </summary>
        /// <param name="id">The hardware panel ID</param>
        /// <returns>The hardware panel with detailed includes</returns>
        public HardwarePanel GetHardwarePanelDetails(int id)
        {
            Logger.LogInformation("Getting hardware panel details by ID: {HardwarePanelId}", id);
            
            var result = FindBy(panel => panel.Id == id)
                .IncludeDetails()
                .FirstOrDefault();
                
            if (result == null)
            {
                Logger.LogWarning("Hardware panel with ID {HardwarePanelId} not found", id);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved hardware panel details with ID {HardwarePanelId}", id);
            }
            
            return result;
        }

        /// <summary>
        /// Deletes a hardware panel and all its associated entities (cascade delete)
        /// </summary>
        /// <param name="id">The hardware panel ID to delete</param>
        public void DeleteHardwarePanel(int id)
        {
            Logger.LogInformation("Deleting hardware panel with ID: {Id} and all associated entities", id);
            
            var hardwarePanel = Get(id);
            if (hardwarePanel != null)
            {
                // Due to cascade delete configuration in CoreDataContext:
                // - HardwareInput records will be automatically deleted
                // - HardwareOutput records will be automatically deleted  
                // - HardwareInputSelector records will be automatically deleted (via HardwareInput)
                // - HardwareOutputSelector records will be automatically deleted (via HardwareOutput)
                
                Delete(hardwarePanel);
                Save(); // Explicit save since base repository no longer auto-saves
                
                Logger.LogInformation("Successfully deleted hardware panel: {Name} (ID: {Id}) and all associated entities", 
                    hardwarePanel.Name, hardwarePanel.Id);
            }
            else
            {
                Logger.LogWarning("Failed to delete hardware panel with ID {Id} - not found", id);
                throw new EntityNotFoundException("HardwarePanel", id);
            }
        }

        /// <summary>
        /// Updates an existing hardware panel
        /// </summary>
        /// <param name="hardwarePanel">The hardware panel to update</param>
        /// <returns>The updated hardware panel</returns>
        public HardwarePanel UpdateHardwarePanel(HardwarePanel hardwarePanel)
        {
            Logger.LogInformation("Updating hardware panel: {Name} (ID: {Id})", hardwarePanel.Name, hardwarePanel.Id);
            
            var result = Update(hardwarePanel, hardwarePanel.Id);
            if (result != null)
            {
                Save(); // Explicit save since base repository no longer auto-saves
                Logger.LogInformation("Successfully updated hardware panel: {Name} (ID: {Id})", hardwarePanel.Name, hardwarePanel.Id);
            }
            
            return result;
        }
    }
}