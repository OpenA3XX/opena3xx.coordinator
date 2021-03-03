using System.Collections.Generic;
using OpenA3XX.Core.Forms;

namespace OpenA3XX.Core.Services
{
    public class FormsService : IFormService
    {
        public IList<FieldConfig> GetSettingsFormFields()
        {
            var settingsForm = new List<FieldConfig>()
            {
                new()
                {
                    Type = "heading", 
                    Label = "RabbitMQ Configuration"
                },
                new ()
                {
                    Type = "input",
                    InputType = "text",
                    Label = "Enter your name",
                    Name = "name",
                    Hint = "This is name hint",
                    Validations = new List<FieldValidatorConfig>()
                    {
                        new ()
                        {
                            Name = "required",
                            Message = "Name is required!"
                        },
                        new ()
                        {
                            Name = "pattern",
                            Pattern = "(localhost|\\b(?:(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)\\.){3}(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)(?::\\d{0,4})?\\b)",
                            Message = "Invalid IP Address or 'localhost'"
                        }
                    }
                },
                new ()
                {
                    Type = "input",
                    InputType = "password",
                    Label = "Enter your name",
                    Name = "password",
                    Hint = "This is password hint",
                },
                new ()
                {
                    Type = "select",
                    Label = "Enter Country",
                    Name = "country",
                    Value = "Comino",
                    Hint = "This is a select hint",
                    Options = new List<string> {"Malta", "Gozo", "Comino", "Ras Zobbi"}
                },
                new ()
                {
                    Type = "radiobutton",
                    Label = "Gender",
                    Name = "gender",
                    Value = "Male",
                    Hint = "This is a radio button hint",
                    Options = new List<string> {"Male", "Female"}
                },
                new ()
                {
                    Type = "date",
                    Label = "Date of Birth",
                    Name = "dob",
                    Hint = "This is a date hint",
                    Validations = new List<FieldValidatorConfig>()
                    {
                        new FieldValidatorConfig()
                        {
                            Name = "required",
                            Message = "Date of Birth Required"
                        }
                    },
                },
                new ()
                {
                    Type = "checkbox",
                    Label = "Accept Terms & Conditions",
                    Name = "terms",
                    Hint = "This is a checkbox hint",
                    Value = "true"
                },
                new ()
                {
                    Type = "button",
                    Label = "Save"
                }
            };
            return settingsForm;
        }
    }
}