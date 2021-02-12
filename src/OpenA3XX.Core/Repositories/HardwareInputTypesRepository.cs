using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareInputTypesRepository : BaseRepository<HardwareInputType>, IHardwareInputTypesRepository
    {
        public HardwareInputTypesRepository(DbContext context) : base(context)
        {
        }
        
        public IList<HardwareInputType> GetAllHardwareInputTypes()
        {
            return GetAll().ToList();
        }
        
    }

    public interface IHardwareInputTypesRepository
    {
        IList<HardwareInputType> GetAllHardwareInputTypes();
    }
}