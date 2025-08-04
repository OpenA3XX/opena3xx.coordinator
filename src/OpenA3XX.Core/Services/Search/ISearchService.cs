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
        /// Gets search suggestions for autocomplete
        /// </summary>
        /// <param name="queryText">Partial query text</param>
        /// <param name="limit">Maximum number of suggestions</param>
        /// <returns>List of search suggestions</returns>
        Task<List<string>> GetSuggestionsAsync(string queryText, int limit = 5);

        /// <summary>
        /// Gets available entity types for search
        /// </summary>
        /// <returns>List of available entity types</returns>
        Task<List<string>> GetAvailableEntityTypesAsync();

        /// <summary>
        /// Gets search statistics
        /// </summary>
        /// <returns>Search statistics</returns>
        Task<SearchStatisticsDto> GetSearchStatisticsAsync();
    }

    /// <summary>
    /// Search statistics for analytics
    /// </summary>
    public class SearchStatisticsDto
    {
        /// <summary>
        /// Total number of searchable entities
        /// </summary>
        public int TotalEntities { get; set; }

        /// <summary>
        /// Breakdown by entity type
        /// </summary>
        public Dictionary<string, int> EntityTypeCounts { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Most popular search terms
        /// </summary>
        public List<string> PopularSearchTerms { get; set; } = new List<string>();

        /// <summary>
        /// Average search response time in milliseconds
        /// </summary>
        public double AverageResponseTimeMs { get; set; }
    }
} 