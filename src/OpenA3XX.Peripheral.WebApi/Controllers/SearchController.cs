using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services.Search;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Controller for unified search functionality across all entity types
    /// Provides macOS Spotlight-like search capabilities
    /// </summary>
    [ApiController]
    [Route("api/search")]
    [Produces("application/json")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        /// <summary>
        /// Initializes a new instance of the SearchController
        /// </summary>
        /// <param name="searchService">Search service for unified search operations</param>
        /// <param name="logger">Logger instance for this controller</param>
        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        /// <summary>
        /// Performs a unified search across all entity types
        /// </summary>
        /// <param name="q">Search query text</param>
        /// <param name="type">Entity type filter (comma-separated)</param>
        /// <param name="limit">Maximum number of results (default: 20)</param>
        /// <param name="offset">Number of results to skip (default: 0)</param>
        /// <param name="minScore">Minimum relevance score (default: 0.1)</param>
        /// <param name="includeInactive">Whether to include inactive entities (default: false)</param>
        /// <param name="sortBy">Sort order (relevance, title, createdDate, updatedDate, entityType)</param>
        /// <param name="includeFacets">Whether to include facets in response (default: true)</param>
        /// <returns>Search results with facets</returns>
        /// <response code="200">Returns search results</response>
        /// <response code="400">If the search parameters are invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Search(
            [FromQuery] string q = "",
            [FromQuery] string type = null,
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0,
            [FromQuery] double minScore = 0.1,
            [FromQuery] bool includeInactive = false,
            [FromQuery] string sortBy = "relevance",
            [FromQuery] bool includeFacets = true)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest(ErrorDto.Create("Search query is required", "MISSING_QUERY"));
            }

            if (limit <= 0 || limit > 100)
            {
                return BadRequest(ErrorDto.Create("Limit must be between 1 and 100", "INVALID_LIMIT"));
            }

            if (offset < 0)
            {
                return BadRequest(ErrorDto.Create("Offset must be non-negative", "INVALID_OFFSET"));
            }

            try
            {
                _logger.LogInformation("Search request: Query='{Query}', Types='{Types}', Limit={Limit}, Offset={Offset}", 
                    q, type, limit, offset);

                var query = new SearchQueryDto
                {
                    Query = q.Trim(),
                    Limit = limit,
                    Offset = offset,
                    MinRelevanceScore = minScore,
                    IncludeInactive = includeInactive,
                    IncludeFacets = includeFacets
                };

                // Parse entity types
                if (!string.IsNullOrWhiteSpace(type))
                {
                    query.EntityTypes = type.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                // Parse sort order
                if (Enum.TryParse<SearchSortOrder>(sortBy, true, out var sortOrder))
                {
                    query.SortOrder = sortOrder;
                }

                var result = await _searchService.SearchAsync(query);

                _logger.LogInformation("Search completed: {TotalResults} results found in {ExecutionTime}ms", 
                    result.TotalResults, result.ExecutionTimeMs);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Search failed for query: {Query}", q);
                return StatusCode(500, ErrorDto.Create("An error occurred while performing the search", "SEARCH_ERROR"));
            }
        }

        /// <summary>
        /// Performs a quick search with minimal parameters
        /// </summary>
        /// <param name="q">Search query text</param>
        /// <param name="limit">Maximum number of results (default: 10)</param>
        /// <returns>Quick search results</returns>
        /// <response code="200">Returns quick search results</response>
        /// <response code="400">If the search query is invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("quick")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> QuickSearch(
            [FromQuery] string q = "",
            [FromQuery] int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest(ErrorDto.Create("Search query is required", "MISSING_QUERY"));
            }

            try
            {
                _logger.LogInformation("Quick search request: Query='{Query}', Limit={Limit}", q, limit);

                var result = await _searchService.QuickSearchAsync(q.Trim(), limit);

                _logger.LogInformation("Quick search completed: {TotalResults} results found", result.TotalResults);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Quick search failed for query: {Query}", q);
                return StatusCode(500, ErrorDto.Create("An error occurred while performing the quick search", "QUICK_SEARCH_ERROR"));
            }
        }

        /// <summary>
        /// Gets available entity types for search
        /// </summary>
        /// <returns>List of available entity types</returns>
        /// <response code="200">Returns available entity types</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("entity-types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetEntityTypes()
        {
            try
            {
                _logger.LogInformation("Get entity types request");

                var entityTypes = await _searchService.GetAvailableEntityTypesAsync();

                _logger.LogInformation("Get entity types completed: {Count} types found", entityTypes.Count);

                return Ok(entityTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get entity types failed");
                return StatusCode(500, ErrorDto.Create("An error occurred while getting entity types", "ENTITY_TYPES_ERROR"));
            }
        }
    }
} 