using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public class AircraftModelRepository : BaseRepository<AircraftModel>, IAircraftModelRepository
    {
        public AircraftModelRepository(DbContext context) : base(context)
        {
            
        }

        public AircraftModel GetById(int id)
        {
            return FindBy(c => c.Id == id).Include(c => c.Manufacturer).First();
        }
    }
}