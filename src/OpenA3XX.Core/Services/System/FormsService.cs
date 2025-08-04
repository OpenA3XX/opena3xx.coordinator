using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Forms;
using OpenA3XX.Core.Services.Hardware;
using OpenA3XX.Core.Services.Integration;
using OpenA3XX.Core.Services.Simulation;

namespace OpenA3XX.Core.Services.System
{
    public class FormsService : IFormService
    {
        private readonly IFlightIntegrationService _flightIntegrationService;
        private readonly IHardwareBoardService _hardwareBoardService;
        private readonly ISimulatorEventService _simulatorEventService;
        private readonly RabbitMQOptions _rabbitMqOptions;
        private readonly SeqOptions _seqOptions;

        public FormsService(
            ISimulatorEventService simulatorEventService,
            IFlightIntegrationService flightIntegrationService,
            IHardwareBoardService hardwareBoardService,
            IOptions<RabbitMQOptions> rabbitMqOptions,
            IOptions<SeqOptions> seqOptions)
        {
            _simulatorEventService = simulatorEventService;
            _flightIntegrationService = flightIntegrationService;
            _hardwareBoardService = hardwareBoardService;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _seqOptions = seqOptions.Value;
        }

        public IList<FieldConfig> GetHardwareInputSelectorToBoardForm(int hardwareInputSelectorId)
        {
            var form = new List<FieldConfig>
            {
                new()
                {
                    FieldType = FieldType.Select,
                    Options = _hardwareBoardService.GetAllHardwareBoards()
                        .Select(c => new KeyValuePair<string, string>(c.Id.ToString(), c.Name)),
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "Hardware Board is Required"
                        }
                    },
                    Label = "Hardware Board Selection",
                    Name = "hardwareBoardId",
                    Hint = "Select Hardware Board that is responsible for such Hardware Input"
                },
                new()
                {
                    FieldType = FieldType.Select,
                    Options = _flightIntegrationService.GetAllHardwareExtenders(),
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "Hardware Board IO Extender is Required"
                        }
                    },
                    Label = "Hardware Board IO Extender Bus Selection",
                    Name = "hardwareBusExtenderId",
                    Hint = "Select Hardware Board IO Extender Bus that is responsible for such Hardware Input"
                },
                new()
                {
                    FieldType = FieldType.Select,
                    Options = _flightIntegrationService.GetAllHardwareExtenderInputOutput(),
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "Hardware Board IO Extender Bit is Required"
                        }
                    },
                    Label = "Hardware Board IO Extender Bit Selection",
                    Name = "hardwareBusExtenderBitId",
                    Hint = "Select Hardware Board IO Extender Bit that is responsible for such Hardware Input"
                },
                new()
                {
                    FieldType = FieldType.Button,
                    Label = "Apply"
                }
            };

            return form;
        }

        public IList<FieldConfig> GetSimLinkForHardwareInputSelectorIdForm(int hardwareInputSelectorId)
        {
            var simulatorEvents = _simulatorEventService.GetAllSimulatorEvents();

            var simLinkForm = new List<FieldConfig>
            {
                new()
                {
                    FieldType = FieldType.Select,
                    Options = _flightIntegrationService.GetAllIntegrationTypes(),
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "Integration Type is Required"
                        }
                    },
                    Label = "Integration Type",
                    Name = "inputIntegrationType",
                    Hint = "Select type of integration to the Simulator Software"
                },
                new()
                {
                    FieldType = FieldType.Select,
                    Options = simulatorEvents.Select(c =>
                        new KeyValuePair<string, string>(c.Id.ToString(), $"{c.FriendlyName} - {c.EventCode}")),
                    Label = "Simulator Event",
                    Name = "simulatorEvent",
                    Hint = "Select event to trigger to the Simulator Software",
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "Simulator Event is Required"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Button,
                    Label = "Apply"
                }
            };

            return simLinkForm;
        }

        public IList<FieldConfig> GetSettingsFormFields()
        {
            var settingsForm = new List<FieldConfig>
            {
                new()
                {
                    FieldType = FieldType.Heading,
                    Label = "RabbitMQ Configuration"
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "RabbitMQ Host Address",
                    Name = "opena3xx-amqp-host",
                    Hint = "RabbitMQ Host Address is the IP Address where RabbitMQ is running.",
                    Value = _rabbitMqOptions.HostName,
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "RabbitMQ Host is Required"
                        },
                        new()
                        {
                            FieldValidationType = FieldValidationType.Pattern,
                            Pattern =
                                "(localhost|\\b(?:(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)\\.){3}(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)(?::\\d{0,4})?\\b)",
                            Message = "Accept only IPv4 Address or localhost"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "RabbitMQ Host Port",
                    Name = "opena3xx-amqp-port",
                    Hint = "RabbitMQ Port on which it is running (amqp protocol)",
                    Value = _rabbitMqOptions.Port.ToString(),
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "RabbitMQ Port is Required"
                        },
                        new()
                        {
                            FieldValidationType = FieldValidationType.Pattern,
                            Pattern =
                                "^()([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5])$",
                            Message = "RabbitMQ Port is required: 1-65535"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "RabbitMQ Username",
                    Name = "opena3xx-amqp-username",
                    Hint = "RabbitMQ Username used for Authentication",
                    Value = _rabbitMqOptions.Username,
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "RabbitMQ Username is Required"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "RabbitMQ Password",
                    Name = "opena3xx-amqp-password",
                    Hint = "RabbitMQ Password used for Authentication",
                    Value = _rabbitMqOptions.Password,
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "RabbitMQ Password is Required"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "RabbitMQ Virtual Host",
                    Name = "opena3xx-amqp-vhost",
                    Hint = "RabbitMQ Virtual Host",
                    Value = _rabbitMqOptions.VirtualHost,
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "RabbitMQ Virtual Host is Required"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "Keep Alive Exchange Bindings",
                    Name = "opena3xx-amqp-keepalive-exchange-bindings-configuration",
                    Hint = "RabbitMQ Keep Alive Exchange Bindings",
                    Value = _rabbitMqOptions.ExchangeBindings.KeepAlive,
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "RabbitM QKeep Alive Exchange Bindings is Required"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "Hardware Input Selectors Events Queue Name",
                    Name = "opena3xx-amqp-hardware-input-selectors-exchange-bindings-configuration",
                    Hint = "RabbitMQ Hardware Input Selectors Exchange Bindings",
                    Value = _rabbitMqOptions.ExchangeBindings.HardwareInputSelectors,
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "RabbitMQ Hardware Input Selectors Exchange Bindings is Required"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Heading,
                    Label = "SEQ Configuration"
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "SEQ Host Address",
                    Name = "opena3xx-logging-seq-host",
                    Hint = "SEQ Host Address is the IP Address where SEQ is running.",
                    Value = _seqOptions.Host,
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "SEQ Host is Required"
                        },
                        new()
                        {
                            FieldValidationType = FieldValidationType.Pattern,
                            Pattern =
                                "(localhost|\\b(?:(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)\\.){3}(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)(?::\\d{0,4})?\\b)",
                            Message = "Accept only IPv4 Address or localhost"
                        }
                    }
                },
                new()
                {
                    FieldType = FieldType.Input,
                    InputFieldType = InputFieldType.Text,
                    Label = "SEQ Host Port",
                    Name = "opena3xx-logging-seq-port",
                    Hint = "SEQ Port on which it is running",
                    Value = _seqOptions.Port.ToString(),
                    Validations = new List<FieldValidatorConfig>
                    {
                        new()
                        {
                            FieldValidationType = FieldValidationType.Required,
                            Message = "SEQ Port is Required"
                        },
                        new()
                        {
                            FieldValidationType = FieldValidationType.Pattern,
                            Pattern =
                                "^()([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5])$",
                            Message = "SEQ Port is required: 1-65535"
                        }
                    }
                }
            };
            return settingsForm;
        }
    }
}