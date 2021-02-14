using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public class HardwarePanelRepository : BaseRepository<HardwarePanel>, IHardwarePanelRepository
    {
        public HardwarePanelRepository(DbContext context) : base(context)
        {
        }

        public IList<HardwarePanel> GetAllHardwarePanels()
        {
            return GetAll().Include(c => c.AircraftModel).ThenInclude(c => c.Manufacturer).ToList();
        }
    }
    
    public interface IHardwarePanelRepository
    {
        IList<HardwarePanel> GetAllHardwarePanels();
    }
}