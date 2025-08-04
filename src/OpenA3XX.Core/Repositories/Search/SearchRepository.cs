using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Search
{
    /// <summary>
    /// Repository for unified search operations across all entity types
    /// </summary>
    public class SearchRepository : ISearchRepository
    {
        private readonly CoreDataContext _context;
        private readonly ILogger<SearchRepository> _logger;

        public SearchRepository(CoreDataContext context, ILogger<SearchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Searches aircraft models with relevance scoring
        /// </summary>
        public async Task<List<SearchResultDto>> SearchAircraftModelsAsync(string query, int limit, int offset)
        {
            var searchTerms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            var results = await _context.AircraftModels
                .Include(am => am.Manufacturer)
                .Where(am => am.IsActive)
                .ToListAsync();

            var searchResults = new List<SearchResultDto>();

            foreach (var aircraft in results)
            {
                var score = CalculateRelevanceScore(aircraft, searchTerms);
                
                if (score > 0)
                {
                    var result = new SearchResultDto
                    {
                        Id = $"aircraft-{aircraft.Id}",
                        EntityType = "AircraftModel",
                        Title = aircraft.Model,
                        Description = aircraft.Description,
                        Manufacturer = aircraft.Manufacturer?.Name,
                        RelevanceScore = score,
                        Snippet = GenerateSnippet(aircraft, searchTerms),
                        CreatedAt = aircraft.CreatedAt,
                        UpdatedAt = aircraft.UpdatedAt,
                        Metadata = new Dictionary<string, object>
                        {
                            ["isActive"] = aircraft.IsActive,
                            ["type"] = aircraft.Type,
                            ["manufacturerId"] = aircraft.ManufacturerId
                        },
                        Actions = new List<SearchResultActionDto>
                        {
                            new SearchResultActionDto
                            {
                                Name = "View",
                                Url = $"/api/aircraft-models/{aircraft.Id}",
                                Method = "GET"
                            },
                            new SearchResultActionDto
                            {
                                Name = "Edit",
                                Url = $"/api/aircraft-models/{aircraft.Id}",
                                Method = "PUT"
                            }
                        }
                    };

                    searchResults.Add(result);
                }
            }

            return searchResults
                .OrderByDescending(r => r.RelevanceScore)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        /// <summary>
        /// Searches hardware panels with relevance scoring
        /// </summary>
        public async Task<List<SearchResultDto>> SearchHardwarePanelsAsync(string query, int limit, int offset)
        {
            var searchTerms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            var results = await _context.HardwarePanels
                .Include(hp => hp.HardwarePanelOwner)
                .Include(hp => hp.AircraftModel)
                .ToListAsync();

            var searchResults = new List<SearchResultDto>();

            foreach (var panel in results)
            {
                var score = CalculateRelevanceScore(panel, searchTerms);
                
                if (score > 0)
                {
                    var result = new SearchResultDto
                    {
                        Id = $"panel-{panel.Id}",
                        EntityType = "HardwarePanel",
                        Title = panel.Name,
                        Description = $"Panel for {panel.AircraftModel?.Model ?? "Unknown Aircraft"}",
                        RelevanceScore = score,
                        Snippet = GenerateSnippet(panel, searchTerms),
                        CreatedAt = DateTime.UtcNow, // Default since no CreatedAt property
                        UpdatedAt = DateTime.UtcNow, // Default since no UpdatedAt property
                        Metadata = new Dictionary<string, object>
                        {
                            ["aircraftModel"] = panel.AircraftModel?.Model,
                            ["cockpitArea"] = panel.CockpitArea.ToString()
                        },
                        Actions = new List<SearchResultActionDto>
                        {
                            new SearchResultActionDto
                            {
                                Name = "View",
                                Url = $"/api/hardware-panels/{panel.Id}",
                                Method = "GET"
                            },
                            new SearchResultActionDto
                            {
                                Name = "Edit",
                                Url = $"/api/hardware-panels/{panel.Id}",
                                Method = "PUT"
                            }
                        }
                    };

                    searchResults.Add(result);
                }
            }

            return searchResults
                .OrderByDescending(r => r.RelevanceScore)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        /// <summary>
        /// Searches hardware boards with relevance scoring
        /// </summary>
        public async Task<List<SearchResultDto>> SearchHardwareBoardsAsync(string query, int limit, int offset)
        {
            var searchTerms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            var results = await _context.HardwareBoards.ToListAsync();

            var searchResults = new List<SearchResultDto>();

            foreach (var board in results)
            {
                var score = CalculateRelevanceScore(board, searchTerms);
                
                if (score > 0)
                {
                    var result = new SearchResultDto
                    {
                        Id = $"board-{board.Id}",
                        EntityType = "HardwareBoard",
                        Title = board.Name,
                        Description = $"Hardware board with {board.Buses?.Count ?? 0} buses",
                        RelevanceScore = score,
                        Snippet = GenerateSnippet(board, searchTerms),
                        CreatedAt = DateTime.UtcNow, // Default since no CreatedAt property
                        UpdatedAt = DateTime.UtcNow, // Default since no UpdatedAt property
                        Metadata = new Dictionary<string, object>
                        {
                            ["busCount"] = board.Buses?.Count ?? 0
                        },
                        Actions = new List<SearchResultActionDto>
                        {
                            new SearchResultActionDto
                            {
                                Name = "View",
                                Url = $"/api/hardware-boards/{board.Id}",
                                Method = "GET"
                            },
                            new SearchResultActionDto
                            {
                                Name = "Edit",
                                Url = $"/api/hardware-boards/{board.Id}",
                                Method = "PUT"
                            }
                        }
                    };

                    searchResults.Add(result);
                }
            }

            return searchResults
                .OrderByDescending(r => r.RelevanceScore)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        /// <summary>
        /// Searches hardware inputs with relevance scoring
        /// </summary>
        public async Task<List<SearchResultDto>> SearchHardwareInputsAsync(string query, int limit, int offset)
        {
            var searchTerms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            var results = await _context.HardwareInputs
                .Include(hi => hi.HardwareInputType)
                .Include(hi => hi.HardwarePanel)
                .ToListAsync();

            var searchResults = new List<SearchResultDto>();

            foreach (var input in results)
            {
                var score = CalculateRelevanceScore(input, searchTerms);
                
                if (score > 0)
                {
                    var result = new SearchResultDto
                    {
                        Id = $"input-{input.Id}",
                        EntityType = "HardwareInput",
                        Title = input.Name,
                        Description = $"Input on panel {input.HardwarePanel?.Name}",
                        RelevanceScore = score,
                        Snippet = GenerateSnippet(input, searchTerms),
                        CreatedAt = DateTime.UtcNow, // Default since no CreatedAt property
                        UpdatedAt = DateTime.UtcNow, // Default since no UpdatedAt property
                        Metadata = new Dictionary<string, object>
                        {
                            ["type"] = input.HardwareInputType?.Name,
                            ["panelName"] = input.HardwarePanel?.Name
                        },
                        Actions = new List<SearchResultActionDto>
                        {
                            new SearchResultActionDto
                            {
                                Name = "View",
                                Url = $"/api/hardware-inputs/{input.Id}",
                                Method = "GET"
                            },
                            new SearchResultActionDto
                            {
                                Name = "Edit",
                                Url = $"/api/hardware-inputs/{input.Id}",
                                Method = "PUT"
                            }
                        }
                    };

                    searchResults.Add(result);
                }
            }

            return searchResults
                .OrderByDescending(r => r.RelevanceScore)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        /// <summary>
        /// Searches hardware outputs with relevance scoring
        /// </summary>
        public async Task<List<SearchResultDto>> SearchHardwareOutputsAsync(string query, int limit, int offset)
        {
            var searchTerms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            var results = await _context.HardwareOutputs
                .Include(ho => ho.HardwareOutputType)
                .Include(ho => ho.HardwarePanel)
                .ToListAsync();

            var searchResults = new List<SearchResultDto>();

            foreach (var output in results)
            {
                var score = CalculateRelevanceScore(output, searchTerms);
                
                if (score > 0)
                {
                    var result = new SearchResultDto
                    {
                        Id = $"output-{output.Id}",
                        EntityType = "HardwareOutput",
                        Title = output.Name,
                        Description = $"Output on panel {output.HardwarePanel?.Name}",
                        RelevanceScore = score,
                        Snippet = GenerateSnippet(output, searchTerms),
                        CreatedAt = DateTime.UtcNow, // Default since no CreatedAt property
                        UpdatedAt = DateTime.UtcNow, // Default since no UpdatedAt property
                        Metadata = new Dictionary<string, object>
                        {
                            ["type"] = output.HardwareOutputType?.Name,
                            ["panelName"] = output.HardwarePanel?.Name
                        },
                        Actions = new List<SearchResultActionDto>
                        {
                            new SearchResultActionDto
                            {
                                Name = "View",
                                Url = $"/api/hardware-outputs/{output.Id}",
                                Method = "GET"
                            },
                            new SearchResultActionDto
                            {
                                Name = "Edit",
                                Url = $"/api/hardware-outputs/{output.Id}",
                                Method = "PUT"
                            }
                        }
                    };

                    searchResults.Add(result);
                }
            }

            return searchResults
                .OrderByDescending(r => r.RelevanceScore)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        /// <summary>
        /// Searches simulator events with relevance scoring
        /// </summary>
        public async Task<List<SearchResultDto>> SearchSimulatorEventsAsync(string query, int limit, int offset)
        {
            var searchTerms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            var results = await _context.SimulatorEvents
                .Include(se => se.SimulatorEventType)
                .Include(se => se.SimulatorEventSdkType)
                .ToListAsync();

            var searchResults = new List<SearchResultDto>();

            foreach (var simEvent in results)
            {
                var score = CalculateRelevanceScore(simEvent, searchTerms);
                
                if (score > 0)
                {
                    var result = new SearchResultDto
                    {
                        Id = $"event-{simEvent.Id}",
                        EntityType = "SimulatorEvent",
                        Title = simEvent.FriendlyName ?? simEvent.EventCode,
                        Description = $"Event: {simEvent.EventName}",
                        RelevanceScore = score,
                        Snippet = GenerateSnippet(simEvent, searchTerms),
                        CreatedAt = DateTime.UtcNow, // Default since no CreatedAt property
                        UpdatedAt = DateTime.UtcNow, // Default since no UpdatedAt property
                        Metadata = new Dictionary<string, object>
                        {
                            ["eventCode"] = simEvent.EventCode,
                            ["eventName"] = simEvent.EventName,
                            ["eventType"] = simEvent.SimulatorEventType.ToString(),
                            ["sdkType"] = simEvent.SimulatorEventSdkType.ToString()
                        },
                        Actions = new List<SearchResultActionDto>
                        {
                            new SearchResultActionDto
                            {
                                Name = "View",
                                Url = $"/api/simulator-events/{simEvent.Id}",
                                Method = "GET"
                            },
                            new SearchResultActionDto
                            {
                                Name = "Edit",
                                Url = $"/api/simulator-events/{simEvent.Id}",
                                Method = "PUT"
                            }
                        }
                    };

                    searchResults.Add(result);
                }
            }

            return searchResults
                .OrderByDescending(r => r.RelevanceScore)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        /// <summary>
        /// Gets search facets for all entity types
        /// </summary>
        public async Task<SearchFacetsDto> GetSearchFacetsAsync(string query)
        {
            var facets = new SearchFacetsDto();

            // Entity type counts
            facets.EntityTypes["AircraftModel"] = await _context.AircraftModels.CountAsync(am => am.IsActive);
            facets.EntityTypes["HardwarePanel"] = await _context.HardwarePanels.CountAsync();
            facets.EntityTypes["HardwareBoard"] = await _context.HardwareBoards.CountAsync();
            facets.EntityTypes["HardwareInput"] = await _context.HardwareInputs.CountAsync();
            facets.EntityTypes["HardwareOutput"] = await _context.HardwareOutputs.CountAsync();
            facets.EntityTypes["SimulatorEvent"] = await _context.SimulatorEvents.CountAsync();

            // Manufacturer facets
            var manufacturers = await _context.Manufacturers
                .Select(m => m.Name)
                .Distinct()
                .ToListAsync();

            foreach (var manufacturer in manufacturers)
            {
                var count = await _context.AircraftModels
                    .CountAsync(am => am.Manufacturer.Name == manufacturer && am.IsActive);
                if (count > 0)
                {
                    facets.Manufacturers[manufacturer] = count;
                }
            }

            // Hardware type facets
            var inputTypes = await _context.HardwareInputTypes
                .Select(it => it.Name)
                .Distinct()
                .ToListAsync();

            foreach (var inputType in inputTypes)
            {
                var count = await _context.HardwareInputs
                    .CountAsync(hi => hi.HardwareInputType.Name == inputType);
                if (count > 0)
                {
                    facets.HardwareTypes[inputType] = count;
                }
            }

            var outputTypes = await _context.HardwareOutputTypes
                .Select(ot => ot.Name)
                .Distinct()
                .ToListAsync();

            foreach (var outputType in outputTypes)
            {
                var count = await _context.HardwareOutputs
                    .CountAsync(ho => ho.HardwareOutputType.Name == outputType);
                if (count > 0)
                {
                    facets.HardwareTypes[outputType] = count;
                }
            }

            return facets;
        }

        /// <summary>
        /// Gets total count for each entity type
        /// </summary>
        public async Task<Dictionary<string, int>> GetEntityTypeCountsAsync()
        {
            return new Dictionary<string, int>
            {
                ["AircraftModel"] = await _context.AircraftModels.CountAsync(am => am.IsActive),
                ["HardwarePanel"] = await _context.HardwarePanels.CountAsync(),
                ["HardwareBoard"] = await _context.HardwareBoards.CountAsync(),
                ["HardwareInput"] = await _context.HardwareInputs.CountAsync(),
                ["HardwareOutput"] = await _context.HardwareOutputs.CountAsync(),
                ["SimulatorEvent"] = await _context.SimulatorEvents.CountAsync(),
                ["Manufacturer"] = await _context.Manufacturers.CountAsync()
            };
        }

        /// <summary>
        /// Calculates relevance score for an entity based on search terms
        /// </summary>
        private double CalculateRelevanceScore(object entity, string[] searchTerms)
        {
            double score = 0.0;
            var searchText = GetSearchableText(entity).ToLowerInvariant();

            foreach (var term in searchTerms)
            {
                var lowerTerm = term.ToLowerInvariant();
                
                // Exact match gets highest score
                if (searchText.Contains(lowerTerm))
                {
                    score += 1.0;
                }
                // Partial match gets medium score
                else if (lowerTerm.Length >= 3 && searchText.Contains(lowerTerm.Substring(0, 3)))
                {
                    score += 0.5;
                }
                // Fuzzy match gets lower score
                else if (CalculateLevenshteinDistance(lowerTerm, searchText) <= 2)
                {
                    score += 0.2;
                }
            }

            return Math.Min(score / searchTerms.Length, 1.0);
        }

        /// <summary>
        /// Gets searchable text from an entity
        /// </summary>
        private string GetSearchableText(object entity)
        {
            return entity switch
            {
                AircraftModel am => $"{am.Model} {am.Type} {am.Description} {am.Manufacturer?.Name}",
                HardwarePanel hp => $"{hp.Name} {hp.AircraftModel?.Model} {hp.CockpitArea}",
                HardwareBoard hb => $"{hb.Name}",
                HardwareInput hi => $"{hi.Name} {hi.HardwareInputType?.Name} {hi.HardwarePanel?.Name}",
                HardwareOutput ho => $"{ho.Name} {ho.HardwareOutputType?.Name} {ho.HardwarePanel?.Name}",
                SimulatorEvent se => $"{se.EventCode} {se.FriendlyName} {se.EventName} {se.SimulatorEventType} {se.SimulatorEventSdkType}",
                _ => entity.ToString() ?? ""
            };
        }

        /// <summary>
        /// Generates a snippet showing matched content
        /// </summary>
        private string GenerateSnippet(object entity, string[] searchTerms)
        {
            var searchableText = GetSearchableText(entity);
            var maxLength = 150;

            if (searchableText.Length <= maxLength)
            {
                return searchableText;
            }

            // Find the best position to start the snippet
            var bestPosition = 0;
            var bestScore = 0;

            for (int i = 0; i <= searchableText.Length - maxLength; i++)
            {
                var textSnippet = searchableText.Substring(i, maxLength);
                var score = searchTerms.Count(term => 
                    textSnippet.ToLowerInvariant().Contains(term.ToLowerInvariant()));
                
                if (score > bestScore)
                {
                    bestScore = score;
                    bestPosition = i;
                }
            }

            var snippet = searchableText.Substring(bestPosition, maxLength);
            return snippet + (bestPosition + maxLength < searchableText.Length ? "..." : "");
        }

        /// <summary>
        /// Calculates Levenshtein distance for fuzzy matching
        /// </summary>
        private int CalculateLevenshteinDistance(string s1, string s2)
        {
            var matrix = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
                matrix[i, 0] = i;

            for (int j = 0; j <= s2.Length; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    var cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[s1.Length, s2.Length];
        }
    }
} 