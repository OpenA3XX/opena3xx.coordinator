using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface ISimulatorEventService
    {
        IList<SimulatorEventDto> GetAllSimulatorEvents();

        IList<SimulatorEventDto> GetByIntegrationType(int integrationTypeId);

        SimulatorEventDto GetSimulatorEventDetails(int simulatorEventId);

        IList<KeyValuePair<int, string>> GetAllIntegrationTypes();

        void SaveSimulatorEventLinking(int simulatorEventId, int hardwareInputId, int hardwareInputSelectorId);
    }
}