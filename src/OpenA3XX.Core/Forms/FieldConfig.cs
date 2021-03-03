using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenA3XX.Core.Forms
{
    public class FieldConfig
    {
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
        
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        
        [JsonProperty("inputType", NullValueHandling = NullValueHandling.Ignore)]
        public string InputType { get; set; }
        
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> Options { get; set; }
        
        [JsonProperty("collections", NullValueHandling = NullValueHandling.Ignore)]
        public IList<object> Collections { get; set; }
        
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
        
        [JsonProperty("validations", NullValueHandling = NullValueHandling.Ignore)]
        public IList<FieldValidatorConfig> Validations { get; set; }
        
        [JsonProperty("hint", NullValueHandling = NullValueHandling.Ignore)]
        public string Hint { get; set; }
    }
}