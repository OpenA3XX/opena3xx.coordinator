using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenA3XX.Core.Forms
{
    public enum InputFieldType
    {
        Text,
        Password,
        Email
    }

    public enum FieldType
    {
        Heading,
        Input,
        Select,
        Checkbox,
        RadioButton,
        Date,
        SliderToggle,
        Slider,
        Button
    }

    public class FieldConfig
    {
        //------------------------------------------------------------------------

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type => FieldType.ToString().ToLowerInvariant();

        [JsonIgnore] public FieldType FieldType { get; set; }
        //------------------------------------------------------------------------

        //------------------------------------------------------------------------

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
        //------------------------------------------------------------------------

        //------------------------------------------------------------------------

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        //------------------------------------------------------------------------
        //------------------------------------------------------------------------

        [JsonProperty("inputType", NullValueHandling = NullValueHandling.Ignore)]
        public string InputType => InputFieldType.ToString().ToLowerInvariant();

        [JsonIgnore] public InputFieldType InputFieldType { get; set; }

        //------------------------------------------------------------------------

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> Options { get; set; }

        [JsonProperty("collections", NullValueHandling = NullValueHandling.Ignore)]
        public IList<object> Collections { get; set; }


        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("validations", NullValueHandling = NullValueHandling.Ignore)]
        public IList<FieldValidatorConfig> Validations { get; set; }

        [JsonProperty("hint", NullValueHandling = NullValueHandling.Ignore)]
        public string Hint { get; set; }

        [JsonProperty("minValue", NullValueHandling = NullValueHandling.Ignore)]

        public int MinValue { get; set; }

        [JsonProperty("maxValue", NullValueHandling = NullValueHandling.Ignore)]

        public int MaxValue { get; set; }

        [JsonProperty("stepValue", NullValueHandling = NullValueHandling.Ignore)]
        public int StepValue { get; set; }
    }
}