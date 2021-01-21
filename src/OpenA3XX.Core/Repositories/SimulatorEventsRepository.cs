using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace OpenA3XX.Core.Repositories
{
    public class SimulatorEvent
    {
        public string CalculatorCode { get; set; }
        
        [JsonProperty("varType")]
        public string SimulatorEventVarType { get; set; }
        
        [JsonProperty("varClassification")]
        public string SimulatorEventVarClassification { get; set; }
        
        [JsonProperty("modelSpecific")]
        public bool IsModelSpecific { get; set; }
    }

    public class SimulatorEventsRepository : ISimulatorEventsRepository
    {
        private IList<SimulatorEvent> _allSimulatorEvents;

        public SimulatorEventsRepository()
        {
            ReadAll();
        }

        private void ReadAll()
        {
            var jsonString = File.ReadAllText("./Configuration/A32NX.json");
            _allSimulatorEvents = JsonConvert.DeserializeObject<IList<SimulatorEvent>>(jsonString);
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
    }
}