using System;
using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    /// <summary>
    /// Represents search query parameters for unified search
    /// </summary>
    public class SearchQueryDto
    {
        /// <summary>
        /// The search query text
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Entity types to include in search (null = all types)
        /// </summary>
        public List<string> EntityTypes { get; set; }

        /// <summary>
        /// Maximum number of results to return
        /// </summary>
        public int Limit { get; set; } = 20;

        /// <summary>
        /// Number of results to skip (for pagination)
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Minimum relevance score (0.0 to 1.0)
        /// </summary>
        public double MinRelevanceScore { get; set; } = 0.1;

        /// <summary>
        /// Whether to include inactive entities
        /// </summary>
        public bool IncludeInactive { get; set; } = false;

        /// <summary>
        /// Date range filter - from date
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Date range filter - to date
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Manufacturer filter (for aircraft models)
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Sort order for results
        /// </summary>
        public SearchSortOrder SortOrder { get; set; } = SearchSortOrder.Relevance;

        /// <summary>
        /// Whether to include facets in the response
        /// </summary>
        public bool IncludeFacets { get; set; } = true;
    }

    /// <summary>
    /// Available sort orders for search results
    /// </summary>
    public enum SearchSortOrder
    {
        /// <summary>
        /// Sort by relevance score (highest first)
        /// </summary>
        Relevance,

        /// <summary>
        /// Sort by title alphabetically
        /// </summary>
        Title,

        /// <summary>
        /// Sort by creation date (newest first)
        /// </summary>
        CreatedDate,

        /// <summary>
        /// Sort by last updated date (newest first)
        /// </summary>
        UpdatedDate,

        /// <summary>
        /// Sort by entity type, then by title
        /// </summary>
        EntityType
    }
} 