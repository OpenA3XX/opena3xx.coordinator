using Newtonsoft.Json;

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
}