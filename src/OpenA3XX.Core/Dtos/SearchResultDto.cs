using System;
using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    /// <summary>
    /// Represents a unified search result across all entity types
    /// </summary>
    public class SearchResultDto
    {
        /// <summary>
        /// Unique identifier for the result
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of entity (e.g., AircraftModel, HardwarePanel, etc.)
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// Display title for the search result
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description or additional context
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Manufacturer (for aircraft models)
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Relevance score (0.0 to 1.0)
        /// </summary>
        public double RelevanceScore { get; set; }

        /// <summary>
        /// Text snippet showing the matched content
        /// </summary>
        public string Snippet { get; set; }

        /// <summary>
        /// Available actions for this result
        /// </summary>
        public List<SearchResultActionDto> Actions { get; set; } = new List<SearchResultActionDto>();

        /// <summary>
        /// When the entity was created
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// When the entity was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents an action available for a search result
    /// </summary>
    public class SearchResultActionDto
    {
        /// <summary>
        /// Name of the action (e.g., "View", "Edit", "Delete")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL to perform the action
        /// </summary>
        public string Url { get; set; }
    }
} 