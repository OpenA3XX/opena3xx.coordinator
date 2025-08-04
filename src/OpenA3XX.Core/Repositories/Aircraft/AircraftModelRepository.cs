using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories.Aircraft
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

        public new IList<AircraftModel> GetAll()
        {
            Logger.LogInformation("Getting all aircraft models");
            
            var result = base.GetAll()
                .IncludeManufacturer()
                .ToList();
                
            Logger.LogInformation("Retrieved {Count} aircraft models", result.Count);
            
            return result;
        }

        public IList<AircraftModel> GetActive()
        {
            Logger.LogInformation("Getting active aircraft models");
            
            var result = FindBy(model => model.IsActive)
                .IncludeManufacturer()
                .ToList();
                
            Logger.LogInformation("Retrieved {Count} active aircraft models", result.Count);
            
            return result;
        }

        public new AircraftModel Add(AircraftModel aircraftModel)
        {
            Logger.LogInformation("Adding new aircraft model: {ModelName}", aircraftModel.Model);
            
            var result = base.Add(aircraftModel);
            Save();
            
            Logger.LogInformation("Successfully added aircraft model with ID {AircraftModelId}", result.Id);
            
            return result;
        }

        public AircraftModel Update(AircraftModel aircraftModel)
        {
            Logger.LogInformation("Updating aircraft model with ID: {AircraftModelId}", aircraftModel.Id);
            
            var result = Update(aircraftModel, aircraftModel.Id);
            Save();
            
            Logger.LogInformation("Successfully updated aircraft model with ID {AircraftModelId}", result.Id);
            
            return result;
        }

        public void Delete(int id)
        {
            Logger.LogInformation("Deleting aircraft model with ID: {AircraftModelId}", id);
            
            var aircraftModel = GetById(id);
            if (aircraftModel != null)
            {
                Delete(aircraftModel);
                Save();
                Logger.LogInformation("Successfully deleted aircraft model with ID {AircraftModelId}", id);
            }
            else
            {
                Logger.LogWarning("Aircraft model with ID {AircraftModelId} not found for deletion", id);
            }
        }
    }
}