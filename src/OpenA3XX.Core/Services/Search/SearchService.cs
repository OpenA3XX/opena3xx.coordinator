using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Repositories.Search;

namespace OpenA3XX.Core.Services.Search
{
    /// <summary>
    /// Service for unified search across all entity types
    /// </summary>
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly ILogger<SearchService> _logger;

        public SearchService(ISearchRepository searchRepository, ILogger<SearchService> logger)
        {
            _searchRepository = searchRepository;
            _logger = logger;
        }

        /// <summary>
        /// Performs a unified search across all entity types
        /// </summary>
        public async Task<SearchResponseDto> SearchAsync(SearchQueryDto query)
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting unified search for query: {Query}", query.Query);

                var allResults = new List<SearchResultDto>();

                // Determine which entity types to search
                var entityTypesToSearch = GetEntityTypesToSearch(query.EntityTypes);

                // Search each entity type in parallel
                var searchTasks = new List<Task<List<SearchResultDto>>>();

                foreach (var entityType in entityTypesToSearch)
                {
                    searchTasks.Add(SearchEntityTypeAsync(entityType, query));
                }

                // Wait for all searches to complete
                var searchResults = await Task.WhenAll(searchTasks);

                // Combine all results
                foreach (var results in searchResults)
                {
                    allResults.AddRange(results);
                }

                // Apply global filters
                allResults = ApplyGlobalFilters(allResults, query);

                // Sort results
                allResults = SortResults(allResults, query.SortOrder);

                // Calculate pagination
                var totalResults = allResults.Count;
                var totalPages = (int)Math.Ceiling((double)totalResults / query.Limit);
                var page = (query.Offset / query.Limit) + 1;

                // Apply pagination
                var paginatedResults = allResults
                    .Skip(query.Offset)
                    .Take(query.Limit)
                    .ToList();

                // Get facets if requested
                var facets = query.IncludeFacets 
                    ? await _searchRepository.GetSearchFacetsAsync(query.Query)
                    : new SearchFacetsDto();

                stopwatch.Stop();

                var response = new SearchResponseDto
                {
                    Query = query.Query,
                    TotalResults = totalResults,
                    Page = page,
                    TotalPages = totalPages,
                    Results = paginatedResults,
                    Facets = facets,
                    ExecutionTimeMs = stopwatch.ElapsedMilliseconds,
                    IsSuccess = true
                };

                _logger.LogInformation("Search completed: {TotalResults} results found in {ExecutionTime}ms", 
                    totalResults, stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Search failed for query: {Query}", query.Query);

                return new SearchResponseDto
                {
                    Query = query.Query,
                    TotalResults = 0,
                    Page = 1,
                    TotalPages = 0,
                    Results = new List<SearchResultDto>(),
                    Facets = new SearchFacetsDto(),
                    ExecutionTimeMs = stopwatch.ElapsedMilliseconds,
                    IsSuccess = false,
                    ErrorMessage = "An error occurred while performing the search"
                };
            }
        }

        /// <summary>
        /// Performs a quick search with minimal parameters
        /// </summary>
        public async Task<SearchResponseDto> QuickSearchAsync(string queryText, int limit = 10)
        {
            var query = new SearchQueryDto
            {
                Query = queryText,
                Limit = limit,
                Offset = 0,
                MinRelevanceScore = 0.1,
                IncludeInactive = false,
                IncludeFacets = false
            };

            return await SearchAsync(query);
        }

        /// <summary>
        /// Gets search suggestions for autocomplete
        /// </summary>
        public async Task<List<string>> GetSuggestionsAsync(string queryText, int limit = 5)
        {
            try
            {
                _logger.LogInformation("Getting search suggestions for: {QueryText}", queryText);

                // Perform a quick search to get suggestions
                var searchResponse = await QuickSearchAsync(queryText, limit * 2);

                var suggestions = new List<string>();

                // Extract unique titles from search results
                foreach (var result in searchResponse.Results)
                {
                    if (!suggestions.Contains(result.Title) && suggestions.Count < limit)
                    {
                        suggestions.Add(result.Title);
                    }
                }

                // If we don't have enough suggestions, add some common terms
                if (suggestions.Count < limit)
                {
                    var commonTerms = GetCommonSearchTerms();
                    foreach (var term in commonTerms)
                    {
                        if (!suggestions.Contains(term) && suggestions.Count < limit)
                        {
                            suggestions.Add(term);
                        }
                    }
                }

                _logger.LogInformation("Generated {Count} search suggestions", suggestions.Count);

                return suggestions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get search suggestions for: {QueryText}", queryText);
                return new List<string>();
            }
        }

        /// <summary>
        /// Gets available entity types for search
        /// </summary>
        public async Task<List<string>> GetAvailableEntityTypesAsync()
        {
            try
            {
                var entityTypeCounts = await _searchRepository.GetEntityTypeCountsAsync();
                return entityTypeCounts.Keys.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get available entity types");
                return new List<string>();
            }
        }

        /// <summary>
        /// Gets search statistics for analytics
        /// </summary>
        public async Task<SearchStatisticsDto> GetSearchStatisticsAsync()
        {
            try
            {
                var entityTypeCounts = await _searchRepository.GetEntityTypeCountsAsync();
                var totalEntities = entityTypeCounts.Values.Sum();

                return new SearchStatisticsDto
                {
                    TotalEntities = totalEntities,
                    EntityTypeCounts = entityTypeCounts,
                    PopularSearchTerms = GetCommonSearchTerms(),
                    AverageResponseTimeMs = 50.0 // This would be calculated from actual metrics
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get search statistics");
                return new SearchStatisticsDto();
            }
        }

        /// <summary>
        /// Searches a specific entity type
        /// </summary>
        private async Task<List<SearchResultDto>> SearchEntityTypeAsync(string entityType, SearchQueryDto query)
        {
            try
            {
                return entityType switch
                {
                    "AircraftModel" => await _searchRepository.SearchAircraftModelsAsync(query.Query, query.Limit * 2, 0),
                    "HardwarePanel" => await _searchRepository.SearchHardwarePanelsAsync(query.Query, query.Limit * 2, 0),
                    "HardwareBoard" => await _searchRepository.SearchHardwareBoardsAsync(query.Query, query.Limit * 2, 0),
                    "HardwareInput" => await _searchRepository.SearchHardwareInputsAsync(query.Query, query.Limit * 2, 0),
                    "HardwareOutput" => await _searchRepository.SearchHardwareOutputsAsync(query.Query, query.Limit * 2, 0),
                    "SimulatorEvent" => await _searchRepository.SearchSimulatorEventsAsync(query.Query, query.Limit * 2, 0),
                    _ => new List<SearchResultDto>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to search entity type: {EntityType}", entityType);
                return new List<SearchResultDto>();
            }
        }

        /// <summary>
        /// Gets the list of entity types to search based on the query
        /// </summary>
        private List<string> GetEntityTypesToSearch(List<string> requestedTypes)
        {
            if (requestedTypes == null || requestedTypes.Count == 0)
            {
                // Search all entity types
                return new List<string>
                {
                    "AircraftModel",
                    "HardwarePanel",
                    "HardwareBoard",
                    "HardwareInput",
                    "HardwareOutput",
                    "SimulatorEvent"
                };
            }

            return requestedTypes;
        }

        /// <summary>
        /// Applies global filters to search results
        /// </summary>
        private List<SearchResultDto> ApplyGlobalFilters(List<SearchResultDto> results, SearchQueryDto query)
        {
            var filteredResults = results;

            // Filter by minimum relevance score
            if (query.MinRelevanceScore > 0)
            {
                filteredResults = filteredResults.Where(r => r.RelevanceScore >= query.MinRelevanceScore).ToList();
            }

            // Filter by date range
            if (query.FromDate.HasValue)
            {
                filteredResults = filteredResults.Where(r => r.CreatedAt >= query.FromDate.Value).ToList();
            }

            if (query.ToDate.HasValue)
            {
                filteredResults = filteredResults.Where(r => r.CreatedAt <= query.ToDate.Value).ToList();
            }

            // Filter by manufacturer
            if (!string.IsNullOrWhiteSpace(query.Manufacturer))
            {
                filteredResults = filteredResults.Where(r => 
                    !string.IsNullOrWhiteSpace(r.Manufacturer) && 
                    r.Manufacturer.Equals(query.Manufacturer, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Filter by active status
            if (!query.IncludeInactive)
            {
                filteredResults = filteredResults.Where(r => 
                    !r.Metadata.ContainsKey("isActive") || 
                    (bool)r.Metadata["isActive"]).ToList();
            }

            return filteredResults;
        }

        /// <summary>
        /// Sorts search results based on the specified sort order
        /// </summary>
        private List<SearchResultDto> SortResults(List<SearchResultDto> results, SearchSortOrder sortOrder)
        {
            return sortOrder switch
            {
                SearchSortOrder.Relevance => results.OrderByDescending(r => r.RelevanceScore).ToList(),
                SearchSortOrder.Title => results.OrderBy(r => r.Title).ToList(),
                SearchSortOrder.CreatedDate => results.OrderByDescending(r => r.CreatedAt).ToList(),
                SearchSortOrder.UpdatedDate => results.OrderByDescending(r => r.UpdatedAt).ToList(),
                SearchSortOrder.EntityType => results.OrderBy(r => r.EntityType).ThenBy(r => r.Title).ToList(),
                _ => results.OrderByDescending(r => r.RelevanceScore).ToList()
            };
        }

        /// <summary>
        /// Gets common search terms for suggestions
        /// </summary>
        private List<string> GetCommonSearchTerms()
        {
            return new List<string>
            {
                "Boeing",
                "Airbus",
                "737",
                "A320",
                "Panel",
                "Input",
                "Output",
                "Button",
                "Switch",
                "LED",
                "Display",
                "SimConnect",
                "FSUIPC",
                "Event",
                "Simulator"
            };
        }
    }
} 