using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Services.Audit
{
    /// <summary>
    /// Service for logging and retrieving audit entries
    /// </summary>
    public class AuditService : IAuditService
    {
        private readonly CoreDataContext _context;
        private readonly ILogger<AuditService> _logger;

        public AuditService(CoreDataContext context, ILogger<AuditService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Logs an entity change
        /// </summary>
        public async Task LogEntityChangeAsync(
            string entityName,
            string action,
            int entityId,
            string oldValues = null,
            string newValues = null,
            string userId = null,
            string ipAddress = null,
            string context = null,
            string httpMethod = null,
            string endpoint = null)
        {
            try
            {
                var auditEntry = new AuditEntry
                {
                    EntityName = entityName,
                    Action = action,
                    EntityId = entityId,
                    OldValues = oldValues,
                    NewValues = newValues,
                    UserId = userId,
                    Timestamp = DateTime.UtcNow,
                    IpAddress = ipAddress,
                    Context = context,
                    HttpMethod = httpMethod,
                    Endpoint = endpoint
                };

                _context.AuditEntries.Add(auditEntry);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Audit entry created - Entity: {EntityName}, Action: {Action}, EntityId: {EntityId}, User: {UserId}",
                    entityName, action, entityId, userId ?? "Unknown");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create audit entry - Entity: {EntityName}, Action: {Action}, EntityId: {EntityId}",
                    entityName, action, entityId);
                throw;
            }
        }

        /// <summary>
        /// Gets audit entries for a specific entity
        /// </summary>
        public async Task<List<AuditEntry>> GetAuditEntriesAsync(
            string entityName,
            int entityId,
            int limit = 50,
            int offset = 0)
        {
            try
            {
                var query = _context.AuditEntries
                    .Where(a => a.EntityName == entityName && a.EntityId == entityId)
                    .OrderByDescending(a => a.Timestamp);

                var results = await query
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {Count} audit entries for entity {EntityName} with ID {EntityId}",
                    results.Count, entityName, entityId);

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve audit entries for entity {EntityName} with ID {EntityId}",
                    entityName, entityId);
                throw;
            }
        }

        /// <summary>
        /// Gets all audit entries with optional filtering
        /// </summary>
        public async Task<List<AuditEntry>> GetAllAuditEntriesAsync(
            string entityName = null,
            string action = null,
            string userId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int limit = 100,
            int offset = 0)
        {
            try
            {
                var query = _context.AuditEntries.AsQueryable();

                // Apply filters
                if (!string.IsNullOrWhiteSpace(entityName))
                {
                    query = query.Where(a => a.EntityName == entityName);
                }

                if (!string.IsNullOrWhiteSpace(action))
                {
                    query = query.Where(a => a.Action == action);
                }

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    query = query.Where(a => a.UserId == userId);
                }

                if (fromDate.HasValue)
                {
                    query = query.Where(a => a.Timestamp >= fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    query = query.Where(a => a.Timestamp <= toDate.Value);
                }

                // Order by timestamp (newest first)
                query = query.OrderByDescending(a => a.Timestamp);

                var results = await query
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {Count} audit entries with filters - EntityName: {EntityName}, Action: {Action}, UserId: {UserId}",
                    results.Count, entityName ?? "All", action ?? "All", userId ?? "All");

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve audit entries with filters");
                throw;
            }
        }
    }
} 