using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    public class AircraftModelRepository : BaseRepository<AircraftModel>, IAircraftModelRepository
    {
        public AircraftModelRepository(DbContext context, ILogger<BaseRepository<AircraftModel>> logger) : base(context, logger)
        {
            
        }

        public AircraftModel GetById(int id)
        {
            Logger.LogInformation("Getting aircraft model by ID: {AircraftModelId}", id);
            
            var result = FindBy(model => model.Id == id)
                .IncludeManufacturer()
                .FirstOrDefault();
                
            if (result == null)
            {
                Logger.LogWarning("Aircraft model with ID {AircraftModelId} not found", id);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved aircraft model with ID {AircraftModelId}", id);
            }
            
            return result;
        }
    }
}