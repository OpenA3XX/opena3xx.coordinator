using System.Collections.Generic;

namespace OpenA3XX.Core.Services.Integration
{
    public interface IFlightIntegrationService
    {
        IList<KeyValuePair<string, string>> GetAllIntegrationTypes();

        IList<KeyValuePair<string, string>> GetAllHardwareExtenders();
        IList<KeyValuePair<string, string>> GetAllHardwareExtenderInputOutput();
    }
}