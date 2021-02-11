using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace OpenA3XX.Core.Dtos
{
    public class HardwareSignalDto
    {
        [JsonProperty("hardware_board_id")]
        public int HardwareBoardId { get; set; }
        
        [JsonProperty("bus")]
        public int Bus { get; set; }
        
        [JsonProperty("signal_on")]
        public int SignalOn { get; set; }
    }
}