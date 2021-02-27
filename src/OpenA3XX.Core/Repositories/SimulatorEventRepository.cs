using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
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