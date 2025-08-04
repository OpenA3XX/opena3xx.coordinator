using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Simulation
{
    public interface ISimulatorEventRepository
    {
        SimulatorEvent AddSimulatorEvent(SimulatorEvent simulatorEvent);

        SimulatorEvent UpdateSimulatorEvent(SimulatorEvent simulatorEvent);

        IList<SimulatorEvent> GetAllSimulatorEvents();

        SimulatorEvent GetSimulatorEventBy(int simulatorEventId);

        SimulatorEvent GetSimulatorEventByEventCode(string eventCode);

        IList<SimulatorEvent> GetAllSimulatorEventsByIntegrationType(int integrationTypeId);
    }
}