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

        public HardwarePanel GetHardwarePanelDetails(int id)
        {
            return FindBy(c => c.Id == id)
                .Include(c=>c.AircraftModel)
                .ThenInclude(c=>c.Manufacturer)
                .Include(c => c.HardwareInput)
                .ThenInclude(c => c.HardwareInputType)
                .Include(c => c.HardwareOutput)
                .ThenInclude(c => c.HardwareOutputType)
                .First();
        }
    }

    public interface IHardwarePanelRepository
    {
        IList<HardwarePanel> GetAllHardwarePanels();

        HardwarePanel GetHardwarePanelDetails(int id);
    }
}