using Newtonsoft.Json;

namespace OpenA3XX.Core.Eventing
{
    public class FlightSimulatorEvent
    {
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("data")]
        public FlightSimulatorEventPayload Payload { get; set; }
        
    }
}