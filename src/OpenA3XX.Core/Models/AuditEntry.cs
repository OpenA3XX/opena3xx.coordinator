using System;
using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    /// <summary>
    /// Represents an audit entry for tracking entity changes
    /// </summary>
    public class AuditEntry
    {
        /// <summary>
        /// Unique identifier for the audit entry
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the entity that was changed
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string EntityName { get; set; }

        /// <summary>
        /// The action performed (Created, Updated, Deleted)
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Action { get; set; }

        /// <summary>
        /// ID of the entity that was changed
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// JSON representation of the old values (for updates/deletes)
        /// </summary>
        public string OldValues { get; set; }

        /// <summary>
        /// JSON representation of the new values (for creates/updates)
        /// </summary>
        public string NewValues { get; set; }

        /// <summary>
        /// ID of the user who made the change (if available)
        /// </summary>
        [MaxLength(100)]
        public string UserId { get; set; }

        /// <summary>
        /// When the change occurred
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// IP address of the request (if available)
        /// </summary>
        [MaxLength(45)]
        public string IpAddress { get; set; }

        /// <summary>
        /// Additional context about the change
        /// </summary>
        [MaxLength(500)]
        public string Context { get; set; }

        /// <summary>
        /// HTTP method that triggered the change (GET, POST, PUT, DELETE)
        /// </summary>
        [MaxLength(10)]
        public string HttpMethod { get; set; }

        /// <summary>
        /// URL endpoint that was called
        /// </summary>
        [MaxLength(500)]
        public string Endpoint { get; set; }
    }
} 