using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Services.Audit
{
    /// <summary>
    /// Service for logging audit entries
    /// </summary>
    public interface IAuditService
    {
        /// <summary>
        /// Logs an entity change
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="action">Action performed (Created, Updated, Deleted)</param>
        /// <param name="entityId">ID of the entity</param>
        /// <param name="oldValues">Old values as JSON (optional)</param>
        /// <param name="newValues">New values as JSON (optional)</param>
        /// <param name="userId">User ID who made the change (optional)</param>
        /// <param name="ipAddress">IP address of the request (optional)</param>
        /// <param name="context">Additional context (optional)</param>
        /// <param name="httpMethod">HTTP method (optional)</param>
        /// <param name="endpoint">Endpoint URL (optional)</param>
        /// <returns>Task representing the async operation</returns>
        Task LogEntityChangeAsync(
            string entityName,
            string action,
            int entityId,
            string oldValues = null,
            string newValues = null,
            string userId = null,
            string ipAddress = null,
            string context = null,
            string httpMethod = null,
            string endpoint = null);

        /// <summary>
        /// Gets audit entries for a specific entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="entityId">ID of the entity</param>
        /// <param name="limit">Maximum number of entries to return</param>
        /// <param name="offset">Number of entries to skip</param>
        /// <returns>Collection of audit entries</returns>
        Task<List<AuditEntry>> GetAuditEntriesAsync(
            string entityName,
            int entityId,
            int limit = 50,
            int offset = 0);

        /// <summary>
        /// Gets all audit entries with optional filtering
        /// </summary>
        /// <param name="entityName">Entity name filter (optional)</param>
        /// <param name="action">Action filter (optional)</param>
        /// <param name="userId">User ID filter (optional)</param>
        /// <param name="fromDate">Start date filter (optional)</param>
        /// <param name="toDate">End date filter (optional)</param>
        /// <param name="limit">Maximum number of entries to return</param>
        /// <param name="offset">Number of entries to skip</param>
        /// <returns>Collection of audit entries</returns>
        Task<List<AuditEntry>> GetAllAuditEntriesAsync(
            string entityName = null,
            string action = null,
            string userId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int limit = 100,
            int offset = 0);
    }
} 