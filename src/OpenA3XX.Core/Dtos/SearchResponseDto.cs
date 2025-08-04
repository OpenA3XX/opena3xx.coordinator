using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    /// <summary>
    /// Represents the complete search response including results and facets
    /// </summary>
    public class SearchResponseDto
    {
        /// <summary>
        /// The original search query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Total number of results found
        /// </summary>
        public int TotalResults { get; set; }

        /// <summary>
        /// Current page number (calculated from offset and limit)
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Search results
        /// </summary>
        public List<SearchResultDto> Results { get; set; } = new List<SearchResultDto>();

        /// <summary>
        /// Search facets for filtering
        /// </summary>
        public SearchFacetsDto Facets { get; set; } = new SearchFacetsDto();

        /// <summary>
        /// Search execution time in milliseconds
        /// </summary>
        public long ExecutionTimeMs { get; set; }

        /// <summary>
        /// Whether the search was successful
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Error message if search failed
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Represents search facets for filtering
    /// </summary>
    public class SearchFacetsDto
    {
        /// <summary>
        /// Facets by entity type
        /// </summary>
        public Dictionary<string, int> EntityTypes { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Facets by manufacturer
        /// </summary>
        public Dictionary<string, int> Manufacturers { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Facets by hardware type
        /// </summary>
        public Dictionary<string, int> HardwareTypes { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Facets by simulator SDK type
        /// </summary>
        public Dictionary<string, int> SimulatorSdkTypes { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Facets by date ranges
        /// </summary>
        public Dictionary<string, int> DateRanges { get; set; } = new Dictionary<string, int>();
    }
} 