using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace OpenA3XX.Core.Repositories.Hardware
{
    public class HardwareOutputTypesRepository : BaseRepository<HardwareOutputType>, IHardwareOutputTypesRepository
    {
        public HardwareOutputTypesRepository(DbContext context, ILogger<BaseRepository<HardwareOutputType>> logger) : base(context, logger)
        {
        }

        public IList<HardwareOutputType> GetAllHardwareOutputTypes()
        {
            return GetAll().ToList();
        }

        public HardwareOutputType GetHardwareOutputTypeBy(int id)
        {
            return Get(id);
        }

        public HardwareOutputType AddHardwareOutputType(HardwareOutputType hardwareOutputType)
        {
            var storedModel = Find(c => c.Name == hardwareOutputType.Name);
            if (storedModel == null) return Add(hardwareOutputType);

            throw new HardwareOutputTypeExistsException(
                $"Hardware Output Type with {hardwareOutputType.Name} already exists");
        }

        public HardwareOutputType UpdateHardwareOutputType(HardwareOutputType hardwareOutputType)
        {
            return Update(hardwareOutputType, hardwareOutputType.Id);
        }

        /// <summary>
        /// Deletes a hardware output type by its ID
        /// </summary>
        /// <param name="id">The hardware output type ID to delete</param>
        public void DeleteHardwareOutputType(int id)
        {
            Logger.LogInformation("Deleting hardware output type with ID: {Id}", id);
            
            var hardwareOutputType = Get(id);
            if (hardwareOutputType != null)
            {
                // Check if there are any HardwareOutput entities using this type
                var hardwareOutputsUsingType = Context.Set<HardwareOutput>()
                    .Where(ho => ho.HardwareOutputTypeId == id)
                    .ToList();
                
                if (hardwareOutputsUsingType.Any())
                {
                    Logger.LogWarning("Cannot delete hardware output type '{Name}' (ID: {Id}) - it is being used by {Count} hardware outputs", 
                        hardwareOutputType.Name, hardwareOutputType.Id, hardwareOutputsUsingType.Count);
                    throw new ValidationException($"Cannot delete hardware output type '{hardwareOutputType.Name}' - it is being used by {hardwareOutputsUsingType.Count} hardware outputs");
                }
                
                Delete(hardwareOutputType);
                Save(); // Explicit save since base repository no longer auto-saves
                
                Logger.LogInformation("Successfully deleted hardware output type: {Name} (ID: {Id})", 
                    hardwareOutputType.Name, hardwareOutputType.Id);
            }
            else
            {
                Logger.LogWarning("Failed to delete hardware output type with ID {Id} - not found", id);
                throw new EntityNotFoundException("HardwareOutputType", id);
            }
        }
    }
}