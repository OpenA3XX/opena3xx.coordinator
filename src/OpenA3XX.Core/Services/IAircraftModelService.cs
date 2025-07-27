using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IAircraftModelService
    {
        IList<AircraftModelDto> GetAllAircraftModels();
        IList<AircraftModelDto> GetActiveAircraftModels();
        AircraftModelDto GetAircraftModelById(int id);
        AircraftModelDto AddAircraftModel(AddAircraftModelDto aircraftModelDto);
        AircraftModelDto UpdateAircraftModel(int id, UpdateAircraftModelDto aircraftModelDto);
        void DeleteAircraftModel(int id);
    }
} 