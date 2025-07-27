using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IAircraftModelRepository
    {
        AircraftModel GetById(int id);
        IList<AircraftModel> GetAll();
        IList<AircraftModel> GetActive();
        AircraftModel Add(AircraftModel aircraftModel);
        AircraftModel Update(AircraftModel aircraftModel);
        void Delete(int id);
    }
}