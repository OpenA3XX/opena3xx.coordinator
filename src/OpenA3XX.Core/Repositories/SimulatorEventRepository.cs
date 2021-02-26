using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public interface ISimulatorEventLinkRepository
    {
        IList<SimulatorEventLink> GetAllSimulatorEventLinks();
    }

    public class SimulatorEventLinkRepository : BaseRepository<SimulatorEventLink>, ISimulatorEventLinkRepository
    {
        public SimulatorEventLinkRepository(DbContext context) : base(context)
        {
        }
        
        public IList<SimulatorEventLink> GetAllSimulatorEventLinks()
        {
            return GetAll()
                .Include(c => c.SimulatorEvent)
                .Include(c => c.HardwareInputSelector)
                .ThenInclude(c => c.HardwareInput)
                .ThenInclude(c => c.HardwareInputType)
                .Include(c => c.HardwareInputSelector)
                .ThenInclude(c => c.HardwareInput)
                .ThenInclude(c=>c.HardwarePanel)
                .Include(c => c.HardwareOutputSelector)
                .ThenInclude(c => c.HardwareOutput)
                .ThenInclude(c => c.HardwareOutputType)
                .ToList();

        }
    }
    
    public class SimulatorEventRepository : BaseRepository<SimulatorEvent>, ISimulatorEventRepository
    {
        public SimulatorEventRepository(DbContext context) : base(context)
        {
        }

        public SimulatorEvent AddSimulatorEvent(SimulatorEvent simulatorEvent)
        {
            return Add(simulatorEvent);
        }

        public IList<SimulatorEvent> GetAllSimulatorEvents()
        {
            return GetAll().ToList();
        }
    }
}