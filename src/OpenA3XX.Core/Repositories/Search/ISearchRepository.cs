using System.Collections.Generic;
using System.Threading.Tasks;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Repositories.Search
{
    /// <summary>
    /// Repository for unified search operations
    /// </summary>
    public interface ISearchRepository
    {
        /// <summary>
        /// Searches aircraft models
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="limit">Maximum results</param>
        /// <param name="offset">Results offset</param>
        /// <returns>Search results for aircraft models</returns>
        Task<List<SearchResultDto>> SearchAircraftModelsAsync(string query, int limit, int offset);

        /// <summary>
        /// Searches hardware panels
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="limit">Maximum results</param>
        /// <param name="offset">Results offset</param>
        /// <returns>Search results for hardware panels</returns>
        Task<List<SearchResultDto>> SearchHardwarePanelsAsync(string query, int limit, int offset);

        /// <summary>
        /// Searches hardware boards
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="limit">Maximum results</param>
        /// <param name="offset">Results offset</param>
        /// <returns>Search results for hardware boards</returns>
        Task<List<SearchResultDto>> SearchHardwareBoardsAsync(string query, int limit, int offset);

        /// <summary>
        /// Searches hardware inputs
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="limit">Maximum results</param>
        /// <param name="offset">Results offset</param>
        /// <returns>Search results for hardware inputs</returns>
        Task<List<SearchResultDto>> SearchHardwareInputsAsync(string query, int limit, int offset);

        /// <summary>
        /// Searches hardware outputs
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="limit">Maximum results</param>
        /// <param name="offset">Results offset</param>
        /// <returns>Search results for hardware outputs</returns>
        Task<List<SearchResultDto>> SearchHardwareOutputsAsync(string query, int limit, int offset);

        /// <summary>
        /// Searches simulator events
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="limit">Maximum results</param>
        /// <param name="offset">Results offset</param>
        /// <returns>Search results for simulator events</returns>
        Task<List<SearchResultDto>> SearchSimulatorEventsAsync(string query, int limit, int offset);

        /// <summary>
        /// Gets search facets for all entity types
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Search facets</returns>
        Task<SearchFacetsDto> GetSearchFacetsAsync(string query);

        /// <summary>
        /// Gets total count for each entity type
        /// </summary>
        /// <returns>Entity type counts</returns>
        Task<Dictionary<string, int>> GetEntityTypeCountsAsync();
    }
} 