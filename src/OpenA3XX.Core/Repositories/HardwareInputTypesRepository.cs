using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    /// <summary>
    /// Repository for managing HardwareInputType entities
    /// </summary>
    public class HardwareInputTypesRepository : BaseRepository<HardwareInputType>, IHardwareInputTypesRepository
    {
        /// <summary>
        /// Initializes a new instance of the HardwareInputTypesRepository
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        public HardwareInputTypesRepository(DbContext context, ILogger<BaseRepository<HardwareInputType>> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Gets all hardware input types
        /// </summary>
        /// <returns>List of all hardware input types</returns>
        public IList<HardwareInputType> GetAllHardwareInputTypes()
        {
            Logger.LogInformation("Getting all hardware input types");
            
            var result = GetAll().ToList();
            
            Logger.LogInformation("Retrieved {Count} hardware input types from database", result.Count);
            
            if (result.Count == 0)
            {
                Logger.LogWarning("No hardware input types found in database - database might be empty or not seeded");
            }
            else
            {
                Logger.LogInformation("Hardware input types found: {Names}", 
                    string.Join(", ", result.Select(x => $"'{x.Name}' (ID: {x.Id})")));
            }
            
            return result;
        }

        /// <summary>
        /// Gets a hardware input type by its ID
        /// </summary>
        /// <param name="id">The hardware input type ID</param>
        /// <returns>The hardware input type or null if not found</returns>
        public HardwareInputType GetHardwareInputTypeBy(int id)
        {
            return Get(id);
        }

        /// <summary>
        /// Adds a new hardware input type
        /// </summary>
        /// <param name="hardwareInputType">The hardware input type to add</param>
        /// <returns>The added hardware input type</returns>
        /// <exception cref="HardwareInputTypeExistsException">Thrown when a hardware input type with the same name already exists</exception>
        public HardwareInputType AddHardwareInputType(HardwareInputType hardwareInputType)
        {
            var storedModel = Find(c => c.Name == hardwareInputType.Name);
            if (storedModel != null)
            {
                throw new HardwareInputTypeExistsException(
                    $"Hardware Input Type with name '{hardwareInputType.Name}' already exists");
            }
            
            var result = Add(hardwareInputType);
            Save(); // Explicit save since base repository no longer auto-saves
            return result;
        }

        /// <summary>
        /// Updates an existing hardware input type
        /// </summary>
        /// <param name="hardwareInputType">The hardware input type to update</param>
        /// <returns>The updated hardware input type</returns>
        public HardwareInputType UpdateHardwareInputType(HardwareInputType hardwareInputType)
        {
            var result = Update(hardwareInputType, hardwareInputType.Id);
            if (result != null)
            {
                Save(); // Explicit save since base repository no longer auto-saves
            }
            return result;
        }
    }
}