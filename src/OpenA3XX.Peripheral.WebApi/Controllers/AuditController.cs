using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Services.Audit;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Controller for audit functionality
    /// </summary>
    [ApiController]
    [Route("api/audit")]
    [Produces("application/json")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _auditService;
        private readonly ILogger<AuditController> _logger;

        public AuditController(IAuditService auditService, ILogger<AuditController> logger)
        {
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets audit entries for a specific entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="entityId">ID of the entity</param>
        /// <param name="limit">Maximum number of entries to return (default: 50)</param>
        /// <param name="offset">Number of entries to skip (default: 0)</param>
        /// <returns>Collection of audit entries</returns>
        /// <response code="200">Returns audit entries</response>
        /// <response code="400">If the parameters are invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("entity/{entityName}/{entityId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuditEntry>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetEntityAuditEntries(
            string entityName,
            int entityId,
            [FromQuery] int limit = 50,
            [FromQuery] int offset = 0)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return BadRequest(ErrorDto.Create("Entity name is required", "MISSING_ENTITY_NAME"));
            }

            if (entityId <= 0)
            {
                return BadRequest(ErrorDto.Create("Entity ID must be positive", "INVALID_ENTITY_ID"));
            }

            if (limit <= 0 || limit > 1000)
            {
                return BadRequest(ErrorDto.Create("Limit must be between 1 and 1000", "INVALID_LIMIT"));
            }

            if (offset < 0)
            {
                return BadRequest(ErrorDto.Create("Offset must be non-negative", "INVALID_OFFSET"));
            }

            try
            {
                _logger.LogInformation("Get audit entries request - Entity: {EntityName}, EntityId: {EntityId}, Limit: {Limit}, Offset: {Offset}",
                    entityName, entityId, limit, offset);

                var auditEntries = await _auditService.GetAuditEntriesAsync(entityName, entityId, limit, offset);

                _logger.LogInformation("Get audit entries completed - Entity: {EntityName}, EntityId: {EntityId}, Count: {Count}",
                    entityName, entityId, auditEntries.Count);

                return Ok(auditEntries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get audit entries failed - Entity: {EntityName}, EntityId: {EntityId}",
                    entityName, entityId);
                return StatusCode(500, ErrorDto.Create("An error occurred while retrieving audit entries", "AUDIT_RETRIEVAL_ERROR"));
            }
        }

        /// <summary>
        /// Gets all audit entries with optional filtering
        /// </summary>
        /// <param name="entityName">Entity name filter (optional)</param>
        /// <param name="action">Action filter (optional)</param>
        /// <param name="userId">User ID filter (optional)</param>
        /// <param name="fromDate">Start date filter (optional)</param>
        /// <param name="toDate">End date filter (optional)</param>
        /// <param name="limit">Maximum number of entries to return (default: 100)</param>
        /// <param name="offset">Number of entries to skip (default: 0)</param>
        /// <returns>Collection of audit entries</returns>
        /// <response code="200">Returns audit entries</response>
        /// <response code="400">If the parameters are invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuditEntry>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetAllAuditEntries(
            [FromQuery] string entityName = null,
            [FromQuery] string action = null,
            [FromQuery] string userId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null,
            [FromQuery] int limit = 100,
            [FromQuery] int offset = 0)
        {
            if (limit <= 0 || limit > 1000)
            {
                return BadRequest(ErrorDto.Create("Limit must be between 1 and 1000", "INVALID_LIMIT"));
            }

            if (offset < 0)
            {
                return BadRequest(ErrorDto.Create("Offset must be non-negative", "INVALID_OFFSET"));
            }

            try
            {
                _logger.LogInformation("Get all audit entries request - EntityName: {EntityName}, Action: {Action}, UserId: {UserId}, Limit: {Limit}, Offset: {Offset}",
                    entityName ?? "All", action ?? "All", userId ?? "All", limit, offset);

                var auditEntries = await _auditService.GetAllAuditEntriesAsync(
                    entityName, action, userId, fromDate, toDate, limit, offset);

                _logger.LogInformation("Get all audit entries completed - Count: {Count}", auditEntries.Count);

                return Ok(auditEntries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get all audit entries failed");
                return StatusCode(500, ErrorDto.Create("An error occurred while retrieving audit entries", "AUDIT_RETRIEVAL_ERROR"));
            }
        }
    }
} 