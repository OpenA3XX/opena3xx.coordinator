using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public class SimulatorEventRepository : BaseRepository<SimulatorEvent>, ISimulatorEventRepository
    {
        public SimulatorEventRepository(DbContext context, ILogger<BaseRepository<SimulatorEvent>> logger) : base(context, logger)
        {
        }

        public SimulatorEvent AddSimulatorEvent(SimulatorEvent simulatorEvent)
        {
            return Add(simulatorEvent);
        }

        public SimulatorEvent UpdateSimulatorEvent(SimulatorEvent simulatorEvent)
        {
            Update(simulatorEvent, simulatorEvent.Id);
            return simulatorEvent;
        }

        public IList<SimulatorEvent> GetAllSimulatorEvents()
        {
            return GetAll().ToList();
        }

        public SimulatorEvent GetSimulatorEventBy(int simulatorEventId)
        {
            return Get(simulatorEventId);
        }
        
        public SimulatorEvent GetSimulatorEventByEventCode(string eventCode)
        {
            return Find(c => c.EventCode.Contains(eventCode));
        }

        public IList<SimulatorEvent> GetAllSimulatorEventsByIntegrationType(int integrationTypeId)
        {
            var integrationType = Enum.Parse<SimulatorEventSdkType>(integrationTypeId.ToString());
            return FindAll(c => c.SimulatorEventSdkType == integrationType).OrderBy(c => c.FriendlyName).ToList();
        }
    }
}