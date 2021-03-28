using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IAircraftModelRepository
    {
        AircraftModel GetById(int id);
    }
}