using System.Collections.Generic;

namespace OpenA3XX.Core.Services
{
    public interface IFlightIntegrationService
    {
        IList<KeyValuePair<string, string>> GetAllIntegrationTypes();
    }
}