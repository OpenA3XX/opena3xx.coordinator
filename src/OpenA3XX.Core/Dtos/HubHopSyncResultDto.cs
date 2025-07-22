using System;

namespace OpenA3XX.Core.Dtos
{
    /// <summary>
    /// Result of synchronizing HubHop presets with local SimulatorEvent database
    /// </summary>
    public class HubHopSyncResultDto
    {
        /// <summary>
        /// Total number of presets fetched from HubHop API
        /// </summary>
        public int TotalFetched { get; set; }
        
        /// <summary>
        /// Number of new SimulatorEvent records added to database
        /// </summary>
        public int RecordsAdded { get; set; }
        
        /// <summary>
        /// Number of existing SimulatorEvent records updated
        /// </summary>
        public int RecordsUpdated { get; set; }
        
        /// <summary>
        /// Number of records skipped (e.g., already up to date)
        /// </summary>
        public int RecordsSkipped { get; set; }
        
        /// <summary>
        /// Number of errors encountered during sync
        /// </summary>
        public int ErrorCount { get; set; }
        
        /// <summary>
        /// When the synchronization was performed
        /// </summary>
        public DateTime SyncDateTime { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Time taken for the synchronization process
        /// </summary>
        public TimeSpan Duration { get; set; }
        
        /// <summary>
        /// Whether the synchronization completed successfully
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Error message if synchronization failed
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
        
        /// <summary>
        /// Additional details or notes about the sync process
        /// </summary>
        public string Details { get; set; } = string.Empty;
    }
} 