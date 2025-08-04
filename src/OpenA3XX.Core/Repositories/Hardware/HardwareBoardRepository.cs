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
    /// Repository for managing HardwareBoard entities
    /// </summary>
    public class HardwareBoardRepository : BaseRepository<HardwareBoard>, IHardwareBoardRepository
    {
        /// <summary>
        /// Initializes a new instance of the HardwareBoardRepository
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        public HardwareBoardRepository(DbContext context, ILogger<BaseRepository<HardwareBoard>> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Adds a new hardware board
        /// </summary>
        /// <param name="hardwareBoard">The hardware board to add</param>
        /// <returns>The added hardware board</returns>
        public HardwareBoard AddHardwareBoard(HardwareBoard hardwareBoard)
        {
            var result = Add(hardwareBoard);
            Save();
            return result;
        }

        /// <summary>
        /// Gets all hardware boards with their complete hierarchy of related entities
        /// </summary>
        /// <returns>List of all hardware boards with full includes</returns>
        public IList<HardwareBoard> GetAllHardwareBoards()
        {
            return GetAll()
                .IncludeFullHierarchy()
                .ToList();
        }

        /// <summary>
        /// Gets a specific hardware board by ID with its complete hierarchy of related entities
        /// </summary>
        /// <param name="id">The hardware board ID</param>
        /// <returns>The hardware board with full includes</returns>
        public HardwareBoard GetByHardwareBoard(int id)
        {
            Logger.LogInformation("Getting hardware board by ID: {HardwareBoardId}", id);
            
            var result = FindBy(board => board.Id == id)
                .IncludeFullHierarchy()
                .FirstOrDefault();
                
            if (result == null)
            {
                Logger.LogWarning("Hardware board with ID {HardwareBoardId} not found", id);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved hardware board with ID {HardwareBoardId}", id);
            }
            
            return result;
        }

        /// <summary>
        /// Saves a new hardware board (alias for AddHardwareBoard)
        /// </summary>
        /// <param name="hardwareBoard">The hardware board to save</param>
        /// <returns>The saved hardware board</returns>
        public HardwareBoard SaveHardwareBoard(HardwareBoard hardwareBoard)
        {
            return AddHardwareBoard(hardwareBoard);
        }

        /// <summary>
        /// Updates an existing hardware board
        /// </summary>
        /// <param name="hardwareBoard">The hardware board to update</param>
        /// <returns>The updated hardware board</returns>
        public HardwareBoard UpdateHardwareBoard(HardwareBoard hardwareBoard)
        {
            var result = Update(hardwareBoard, hardwareBoard.Id);
            if (result != null)
            {
                Save();
            }
            return result;
        }

        /// <summary>
        /// Deletes a hardware board and all its associated entities (cascade delete)
        /// </summary>
        /// <param name="id">The hardware board ID to delete</param>
        public void DeleteHardwareBoard(int id)
        {
            Logger.LogInformation("Deleting hardware board with ID: {Id} and all associated entities", id);
            
            var hardwareBoard = Get(id);
            if (hardwareBoard != null)
            {
                // Due to cascade delete configuration in CoreDataContext:
                // - IOExtenderBus records will be automatically deleted
                // - IOExtenderBit records will be automatically deleted (via IOExtenderBus)
                
                Delete(hardwareBoard);
                Save(); // Explicit save since base repository no longer auto-saves
                
                Logger.LogInformation("Successfully deleted hardware board: {Name} (ID: {Id}) and all associated entities", 
                    hardwareBoard.Name, hardwareBoard.Id);
            }
            else
            {
                Logger.LogWarning("Failed to delete hardware board with ID {Id} - not found", id);
                throw new EntityNotFoundException("HardwareBoard", id);
            }
        }
    }
}