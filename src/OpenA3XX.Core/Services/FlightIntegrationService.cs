using System.Collections.Generic;

namespace OpenA3XX.Core.Services
{
    public class FlightIntegrationService : IFlightIntegrationService
    {
        public IList<KeyValuePair<string, string>> GetAllIntegrationTypes()
        {
            var integrationTypes = new List<KeyValuePair<string, string>>()
            {
                new("1", "SimConnect: Direct"),
                new("2", "SimConnect: OpenA3XX WASM Gauge"),
                new("3", "FSUIPC"),
                new("4", "Websockets")
            };

            return integrationTypes;
        }
    }
}