using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories.Hardware
{
    public class HardwareOutputSelectorRepository : BaseRepository<HardwareOutputSelector>,
        IHardwareOutputSelectorRepository
    {
        public HardwareOutputSelectorRepository(DbContext context, ILogger<BaseRepository<HardwareOutputSelector>> logger) : base(context, logger)
        {
        }


        public IList<HardwareOutputSelector> GetAllOutputSelectors()
        {
            return GetAll().ToList();
        }

        public HardwareOutputSelector GetHardwareOutputSelectorBy(int hardwareOutputSelectorId)
        {
            Logger.LogInformation("Getting hardware output selector by ID: {HardwareOutputSelectorId}", hardwareOutputSelectorId);
            
            var result = FindBy(c => c.Id == hardwareOutputSelectorId).FirstOrDefault();
            
            if (result == null)
            {
                Logger.LogWarning("Hardware output selector with ID {HardwareOutputSelectorId} not found", hardwareOutputSelectorId);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved hardware output selector with ID {HardwareOutputSelectorId}", hardwareOutputSelectorId);
            }
            
            return result;
        }
        
        

        public HardwareOutputSelector AddHardwareOutputSelector(HardwareOutputSelector hardwareOutputSelector)
        {
            Logger.LogInformation("Adding new hardware output selector: {Name} for hardware output {HardwareOutputId}", 
                hardwareOutputSelector.Name, hardwareOutputSelector.HardwareOutputId);
            
            var result = Add(hardwareOutputSelector);
            Save(); // Explicit save since base repository no longer auto-saves
            
            Logger.LogInformation("Successfully added hardware output selector: {Name} (ID: {Id}) for hardware output {HardwareOutputId}", 
                result.Name, result.Id, result.HardwareOutputId);
            
            return result;
        }

        public HardwareOutputSelector UpdateHardwareOutputSelector(HardwareOutputSelector hardwareOutputSelector)
        {
            return Update(hardwareOutputSelector, hardwareOutputSelector.Id);
        }

        /// <summary>
        /// Deletes a hardware output selector by its ID
        /// </summary>
        /// <param name="id">The hardware output selector ID to delete</param>
        public void DeleteHardwareOutputSelector(int id)
        {
            Logger.LogInformation("Deleting hardware output selector with ID: {Id}", id);
            
            var hardwareOutputSelector = Get(id);
            if (hardwareOutputSelector != null)
            {
                Delete(hardwareOutputSelector);
                Save(); // Explicit save since base repository no longer auto-saves
                
                Logger.LogInformation("Successfully deleted hardware output selector: {Name} (ID: {Id})", 
                    hardwareOutputSelector.Name, hardwareOutputSelector.Id);
            }
            else
            {
                Logger.LogWarning("Failed to delete hardware output selector with ID {Id} - not found", id);
                throw new OpenA3XX.Core.Exceptions.EntityNotFoundException("HardwareOutputSelector", id);
            }
        }
    }
}