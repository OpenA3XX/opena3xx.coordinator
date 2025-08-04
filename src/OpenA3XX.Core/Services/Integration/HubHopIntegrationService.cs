using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Simulation;

namespace OpenA3XX.Core.Services.Integration
{
    /// <summary>
    /// Service implementation for integrating with HubHop FS2020 presets API
    /// </summary>
    public class HubHopIntegrationService : IHubHopIntegrationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HubHopIntegrationService> _logger;
        private readonly HubHopApiSettings _settings;
        private readonly IMapper _mapper;
        private readonly ISimulatorEventRepository _simulatorEventRepository;

        public HubHopIntegrationService(
            HttpClient httpClient,
            ILogger<HubHopIntegrationService> logger,
            IOptions<ExternalServicesOptions> options,
            IMapper mapper,
            ISimulatorEventRepository simulatorEventRepository)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _simulatorEventRepository = simulatorEventRepository ?? throw new ArgumentNullException(nameof(simulatorEventRepository));
            _settings = options?.Value?.HubHopApi ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc />
        public async Task<IList<HubHopPresetDto>> GetAllPresetsAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Fetching all presets from HubHop API: {BaseUrl}/{Endpoint}", 
                _settings.BaseUrl, _settings.PresetsEndpoint);

            try
            {
                var url = $"{_settings.BaseUrl.TrimEnd('/')}/{_settings.PresetsEndpoint}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("HubHop API request failed with status: {StatusCode} - {ReasonPhrase}", 
                        response.StatusCode, response.ReasonPhrase);
                    throw new HttpRequestException($"HubHop API request failed: {response.StatusCode} - {response.ReasonPhrase}");
                }

                var jsonContent = await response.Content.ReadAsStringAsync();
                var presets = JsonSerializer.Deserialize<List<HubHopPresetDto>>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                stopwatch.Stop();
                _logger.LogInformation("Successfully fetched {Count} presets from HubHop API in {Duration}ms", 
                    presets?.Count ?? 0, stopwatch.ElapsedMilliseconds);

                return presets ?? new List<HubHopPresetDto>();
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Failed to fetch presets from HubHop API after {Duration}ms", 
                    stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IList<SimulatorEventDto>> GetPresetsAsSimulatorEventsAsync()
        {
            _logger.LogInformation("Converting HubHop presets to SimulatorEvent DTOs");

            var presets = await GetAllPresetsAsync();
            var simulatorEvents = new List<SimulatorEventDto>();

            foreach (var preset in presets)
            {
                try
                {
                    var simulatorEvent = MapPresetToSimulatorEvent(preset);
                    simulatorEvents.Add(simulatorEvent);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to map HubHop preset to SimulatorEvent: {PresetId} - {PresetPath}", 
                        preset.Id, preset.Path);
                }
            }

            _logger.LogInformation("Successfully converted {ConvertedCount} out of {TotalCount} HubHop presets to SimulatorEvents", 
                simulatorEvents.Count, presets.Count);

            return simulatorEvents;
        }

        /// <inheritdoc />
        public async Task<HubHopSyncResultDto> SynchronizePresetsAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            var result = new HubHopSyncResultDto();

            _logger.LogInformation("Starting HubHop presets synchronization");

            try
            {
                // Fetch presets from HubHop API
                var presets = await GetAllPresetsAsync();
                result.TotalFetched = presets.Count;

                _logger.LogInformation("Processing {Count} presets for database synchronization", presets.Count);

                foreach (var preset in presets)
                {
                    try
                    {
                        ProcessPresetForSync(preset, result);
                    }
                    catch (Exception ex)
                    {
                        result.ErrorCount++;
                        _logger.LogError(ex, "Error processing preset {PresetId} - {PresetPath}", 
                            preset.Id, preset.Path);
                    }
                }

                stopwatch.Stop();
                result.Duration = stopwatch.Elapsed;
                result.Success = result.ErrorCount == 0;
                result.Details = $"Synchronized {result.TotalFetched} presets: {result.RecordsAdded} added, {result.RecordsUpdated} updated, {result.RecordsSkipped} skipped, {result.ErrorCount} errors";

                _logger.LogInformation("HubHop synchronization completed: {Details}", result.Details);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                result.Duration = stopwatch.Elapsed;
                result.Success = false;
                result.ErrorMessage = ex.Message;
                
                _logger.LogError(ex, "HubHop synchronization failed after {Duration}ms", 
                    stopwatch.ElapsedMilliseconds);
                
                return result;
            }
        }

        /// <inheritdoc />
        public async Task<IList<HubHopPresetDto>> GetPresetsByVendorAsync(string vendor)
        {
            _logger.LogInformation("Fetching HubHop presets for vendor: {Vendor}", vendor);
            
            var allPresets = await GetAllPresetsAsync();
            var filteredPresets = allPresets.Where(p => 
                p.Vendor.Equals(vendor, StringComparison.OrdinalIgnoreCase)).ToList();
            
            _logger.LogInformation("Found {Count} presets for vendor {Vendor}", filteredPresets.Count, vendor);
            
            return filteredPresets;
        }

        /// <inheritdoc />
        public async Task<IList<HubHopPresetDto>> GetPresetsByAircraftAsync(string aircraft)
        {
            _logger.LogInformation("Fetching HubHop presets for aircraft: {Aircraft}", aircraft);
            
            var allPresets = await GetAllPresetsAsync();
            var filteredPresets = allPresets.Where(p => 
                p.Aircraft.Equals(aircraft, StringComparison.OrdinalIgnoreCase)).ToList();
            
            _logger.LogInformation("Found {Count} presets for aircraft {Aircraft}", filteredPresets.Count, aircraft);
            
            return filteredPresets;
        }

        /// <inheritdoc />
        public async Task<bool> TestConnectionAsync()
        {
            _logger.LogInformation("Testing connection to HubHop API");

            try
            {
                var url = $"{_settings.BaseUrl.TrimEnd('/')}/{_settings.PresetsEndpoint}";
                var response = await _httpClient.GetAsync(url);
                
                var isSuccess = response.IsSuccessStatusCode;
                _logger.LogInformation("HubHop API connection test result: {Success} (Status: {StatusCode})", 
                    isSuccess, response.StatusCode);
                
                return isSuccess;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HubHop API connection test failed");
                return false;
            }
        }

        /// <summary>
        /// Maps a HubHop preset to a SimulatorEvent DTO
        /// </summary>
        private SimulatorEventDto MapPresetToSimulatorEvent(HubHopPresetDto preset)
        {
            return new SimulatorEventDto
            {
                EventCode = preset.Code,
                EventName = preset.Path,
                FriendlyName = preset.Label,
                SimulatorEventSdkType = DetermineSimulatorEventSdkType(preset.Code),
                SimulatorEventType = DetermineSimulatorEventType(preset.PresetType),
                // Additional metadata could be stored in a JSON field or separate table
            };
        }

        /// <summary>
        /// Determines the SDK type based on the preset code
        /// </summary>
        private SimulatorEventSdkType DetermineSimulatorEventSdkType(string code)
        {
            if (string.IsNullOrEmpty(code))
                return SimulatorEventSdkType.SimConnectDirect;

            // Analyze the code format to determine SDK type
            if (code.Contains(">K:") || code.Contains(">B:"))
                return SimulatorEventSdkType.SimConnectDirect;
            
            if (code.Contains(">L:") || code.Contains(">H:"))
                return SimulatorEventSdkType.OpenA3XXWasmGauge; // Could be WASM or direct SimConnect

            return SimulatorEventSdkType.SimConnectDirect; // Default fallback
        }

        /// <summary>
        /// Determines the event type based on preset type
        /// </summary>
        private SimulatorEventType DetermineSimulatorEventType(string presetType)
        {
            return presetType?.ToLowerInvariant() switch
            {
                "input" => SimulatorEventType.InputToSimulator,
                "output" => SimulatorEventType.OutputFromSimulator,
                _ => SimulatorEventType.InputToSimulator // Default to input
            };
        }

        /// <summary>
        /// Processes a single preset for database synchronization
        /// </summary>
        private Task ProcessPresetForSync(HubHopPresetDto preset, HubHopSyncResultDto result)
        {
            // Check if a SimulatorEvent with this code already exists
            var existingEvent = _simulatorEventRepository.GetSimulatorEventByEventCode(preset.Code);

            if (existingEvent != null)
            {
                // Update existing event if HubHop version is newer
                if (ShouldUpdateEvent(existingEvent, preset))
                {
                    UpdateSimulatorEventFromPreset(existingEvent, preset);
                    _simulatorEventRepository.UpdateSimulatorEvent(existingEvent);
                    result.RecordsUpdated++;
                    
                    _logger.LogDebug("Updated SimulatorEvent from HubHop preset: {EventCode}", preset.Code);
                }
                else
                {
                    result.RecordsSkipped++;
                }
            }
            else
            {
                // Create new SimulatorEvent
                var newEvent = MapPresetToSimulatorEventModel(preset);
                _simulatorEventRepository.AddSimulatorEvent(newEvent);
                result.RecordsAdded++;
                
                _logger.LogDebug("Added new SimulatorEvent from HubHop preset: {EventCode}", preset.Code);
            }
            
            return Task.CompletedTask;
        }

        /// <summary>
        /// Determines if an existing SimulatorEvent should be updated with HubHop data
        /// </summary>
        private bool ShouldUpdateEvent(SimulatorEvent existingEvent, HubHopPresetDto preset)
        {
            // Update if HubHop has more recent information or better metadata
            return string.IsNullOrEmpty(existingEvent.FriendlyName) || 
                   existingEvent.FriendlyName.Length < preset.Label.Length;
        }

        /// <summary>
        /// Updates an existing SimulatorEvent with data from HubHop preset
        /// </summary>
        private void UpdateSimulatorEventFromPreset(SimulatorEvent simulatorEvent, HubHopPresetDto preset)
        {
            if (string.IsNullOrEmpty(simulatorEvent.FriendlyName) || 
                simulatorEvent.FriendlyName.Length < preset.Label.Length)
            {
                simulatorEvent.FriendlyName = preset.Label;
            }

            if (string.IsNullOrEmpty(simulatorEvent.EventName))
            {
                simulatorEvent.EventName = preset.Path;
            }
        }

        /// <summary>
        /// Maps HubHop preset to SimulatorEvent model
        /// </summary>
        private SimulatorEvent MapPresetToSimulatorEventModel(HubHopPresetDto preset)
        {
            return new SimulatorEvent
            {
                EventCode = preset.Code,
                EventName = preset.Path,
                FriendlyName = preset.Label,
                SimulatorEventSdkType = DetermineSimulatorEventSdkType(preset.Code),
                SimulatorEventType = DetermineSimulatorEventType(preset.PresetType)
            };
        }
    }
} 