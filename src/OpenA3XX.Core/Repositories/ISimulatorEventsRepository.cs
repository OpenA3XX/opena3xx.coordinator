using System.Collections.Generic;

namespace OpenA3XX.Core.Repositories
{
    public interface ISimulatorEventsRepository
    {
        IList<SimulatorEvent> GetAll();
        SimulatorEvent GetByCalculatorCode(string calculatorCode);
        IList<SimulatorEvent> GetByEventVarType(string simulatorEventVarType);
        IList<SimulatorEvent> GetByEventVarClassification(string simulatorEventVarClassification);
    }
}