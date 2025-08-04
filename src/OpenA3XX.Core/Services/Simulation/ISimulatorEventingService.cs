using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services.Simulation
{
    public interface ISimulatorEventingService
    {
        void SendSimulatorTestEvent(SimulatorEventDto simulatorEventDto);
    }
}