using Newtonsoft.Json;

namespace OpenA3XX.Core.Forms
{
    public class FieldValidatorConfig
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name => FieldValidationType.ToString().ToLowerInvariant();

        [JsonProperty("pattern", NullValueHandling = NullValueHandling.Ignore)]
        public string Pattern { get; set; }

        [JsonIgnore] public FieldValidationType FieldValidationType { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public enum FieldValidationType
    {
        Required,
        Pattern
    }
}