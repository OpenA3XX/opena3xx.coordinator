using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace OpenA3XX.Core.Repositories
{
    public class SimulatorEventsRepository : ISimulatorEventsRepository
    {
        private IList<SimulatorEvent> _allSimulatorEvents;

        public SimulatorEventsRepository()
        {
            ReadAll();
        }

        public IList<SimulatorEvent> GetAll()
        {
            return _allSimulatorEvents;
        }

        public SimulatorEvent GetByCalculatorCode(string calculatorCode)
        {
            return _allSimulatorEvents.FirstOrDefault(c => c.CalculatorCode == calculatorCode);
        }

        public IList<SimulatorEvent> GetByEventVarType(string simulatorEventVarType)
        {
            return _allSimulatorEvents.Where(c => c.SimulatorEventVarType == simulatorEventVarType).ToList();
        }

        public IList<SimulatorEvent> GetByEventVarClassification(string simulatorEventVarClassification)
        {
            return _allSimulatorEvents.Where(c => c.SimulatorEventVarClassification == simulatorEventVarClassification).ToList();
        }

        private void ReadAll()
        {
            var jsonString = File.ReadAllText("./Configuration/A32NX.json");
            _allSimulatorEvents = JsonConvert.DeserializeObject<IList<SimulatorEvent>>(jsonString);
        }
    }
}