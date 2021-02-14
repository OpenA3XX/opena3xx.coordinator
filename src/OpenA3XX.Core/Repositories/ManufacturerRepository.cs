using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public class ManufacturerRepository : BaseRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(DbContext context) : base(context)
        {
        }

        public IList<Manufacturer> GetAllManufacturers()
        {
            return GetAll()
                .Include(c => c.AircraftModels)
                .ThenInclude(c => c.HardwarePanels)
                .ThenInclude(c => c.HardwareInput)
                .ThenInclude(c => c.HardwareInputType)
                .Include(c => c.AircraftModels)
                .ThenInclude(c => c.HardwarePanels)
                .ThenInclude(c => c.HardwareOutput)
                .ThenInclude(c => c.HardwareOutputType)
                .ToList();
        }
    }
}