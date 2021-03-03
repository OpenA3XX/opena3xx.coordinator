using Newtonsoft.Json;

namespace OpenA3XX.Core.Forms
{
    public class FieldValidatorConfig
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("pattern", NullValueHandling = NullValueHandling.Ignore)]
        public string Pattern { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}