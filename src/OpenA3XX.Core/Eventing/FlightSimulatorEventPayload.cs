using Newtonsoft.Json;

namespace OpenA3XX.Core.Eventing
{
    public class FlightSimulatorEventPayload
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("value")] public string Value { get; set; }
    }
}