using System.Collections.Generic;

namespace OpenA3XX.Core.Services
{
    public class FlightIntegrationService : IFlightIntegrationService
    {
        public IList<KeyValuePair<string, string>> GetAllIntegrationTypes()
        {
            var integrationTypes = new List<KeyValuePair<string, string>>
            {
                new("1", "SimConnect: Direct"),
                new("2", "SimConnect: OpenA3XX WASM Gauge"),
                new("3", "FSUIPC"),
                new("4", "Websockets")
            };

            return integrationTypes;
        }

        public IList<KeyValuePair<string, string>> GetAllHardwareExtenders()
        {
            var list = new List<KeyValuePair<string, string>>();
            for (var bus = 0; bus <= 7; bus++)
                list.Add(new KeyValuePair<string, string>(bus.ToString(), $"Extender {bus}"));

            return list;
        }

        public IList<KeyValuePair<string, string>> GetAllHardwareExtenderInputOutput()
        {
            var list = new List<KeyValuePair<string, string>>();
            for (var bit = 0; bit <= 15; bit++)
                list.Add(new KeyValuePair<string, string>(bit.ToString(), $"Bit {bit}"));

            return list;
        }
    }
}