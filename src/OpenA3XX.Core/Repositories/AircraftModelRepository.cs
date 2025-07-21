using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    public class AircraftModelRepository : BaseRepository<AircraftModel>, IAircraftModelRepository
    {
        public AircraftModelRepository(DbContext context) : base(context)
        {
            
        }

        public AircraftModel GetById(int id)
        {
            return FindBy(model => model.Id == id)
                .IncludeManufacturer()
                .First();
        }
    }
}