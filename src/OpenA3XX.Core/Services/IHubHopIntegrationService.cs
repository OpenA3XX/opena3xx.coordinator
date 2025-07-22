using System.Collections.Generic;
using System.Threading.Tasks;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    /// <summary>
    /// Service interface for integrating with HubHop FS2020 presets API
    /// </summary>
    public interface IHubHopIntegrationService
    {
        /// <summary>
        /// Fetches all presets from the HubHop API
        /// </summary>
        /// <returns>Collection of HubHop presets</returns>
        Task<IList<HubHopPresetDto>> GetAllPresetsAsync();
        
        /// <summary>
        /// Fetches presets from HubHop API and converts them to SimulatorEvent entities
        /// </summary>
        /// <returns>Collection of mapped SimulatorEvent DTOs</returns>
        Task<IList<SimulatorEventDto>> GetPresetsAsSimulatorEventsAsync();
        
        /// <summary>
        /// Synchronizes HubHop presets with local SimulatorEvent database
        /// Updates existing entries and adds new ones
        /// </summary>
        /// <returns>Synchronization result with counts of added/updated records</returns>
        Task<HubHopSyncResultDto> SynchronizePresetsAsync();
        
        /// <summary>
        /// Fetches presets filtered by aircraft vendor
        /// </summary>
        /// <param name="vendor">Vendor name to filter by</param>
        /// <returns>Filtered collection of presets</returns>
        Task<IList<HubHopPresetDto>> GetPresetsByVendorAsync(string vendor);
        
        /// <summary>
        /// Fetches presets filtered by aircraft model
        /// </summary>
        /// <param name="aircraft">Aircraft model to filter by</param>
        /// <returns>Filtered collection of presets</returns>
        Task<IList<HubHopPresetDto>> GetPresetsByAircraftAsync(string aircraft);
        
        /// <summary>
        /// Tests the connection to HubHop API
        /// </summary>
        /// <returns>True if API is accessible, false otherwise</returns>
        Task<bool> TestConnectionAsync();
    }
} 