using System.Collections.Generic;
using System.Threading.Tasks;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services.Search
{
    /// <summary>
    /// Service for unified search across all entity types
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Performs a unified search across all entity types
        /// </summary>
        /// <param name="query">Search query parameters</param>
        /// <returns>Search response with results and facets</returns>
        Task<SearchResponseDto> SearchAsync(SearchQueryDto query);

        /// <summary>
        /// Performs a quick search with minimal parameters
        /// </summary>
        /// <param name="queryText">Search query text</param>
        /// <param name="limit">Maximum number of results</param>
        /// <returns>Search response with results</returns>
        Task<SearchResponseDto> QuickSearchAsync(string queryText, int limit = 10);

        /// <summary>
        /// Gets available entity types for search
        /// </summary>
        /// <returns>List of available entity types</returns>
        Task<List<string>> GetAvailableEntityTypesAsync();
    }
} 