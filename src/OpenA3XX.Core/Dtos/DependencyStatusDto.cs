using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    /// <summary>
    /// DTO for overall dependency status response
    /// </summary>
    public class DependencyStatusDto
    {
        /// <summary>
        /// Overall system health status
        /// </summary>
        public bool IsHealthy { get; set; }
        
        /// <summary>
        /// Individual dependency statuses
        /// </summary>
        public Dictionary<string, DependencyDetailDto> Dependencies { get; set; } = new Dictionary<string, DependencyDetailDto>();
        
        /// <summary>
        /// Timestamp when the status was checked
        /// </summary>
        public System.DateTime CheckedAt { get; set; } = System.DateTime.UtcNow;
    }

    /// <summary>
    /// DTO for individual dependency status
    /// </summary>
    public class DependencyDetailDto
    {
        /// <summary>
        /// Whether the dependency is running/accessible
        /// </summary>
        public bool IsRunning { get; set; }
        
        /// <summary>
        /// Status message or error details
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// Additional metadata about the dependency
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
} 