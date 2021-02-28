using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface ISimulatorEventService
    {
        IList<SimulatorEventDto> GetAllSimulatorEvents();
    }
}