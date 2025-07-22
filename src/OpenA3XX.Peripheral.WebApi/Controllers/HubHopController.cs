using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Controller for HubHop FS2020 presets integration
    /// </summary>
    [ApiController]
    [Route("hubhop")]
    [Produces("application/json")]
    public class HubHopController : ControllerBase
    {
        private readonly ILogger<HubHopController> _logger;
        private readonly IHubHopIntegrationService _hubHopService;

        public HubHopController(
            ILogger<HubHopController> logger,
            IHubHopIntegrationService hubHopService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubHopService = hubHopService ?? throw new ArgumentNullException(nameof(hubHopService));
        }

        /// <summary>
        /// Test connection to HubHop API
        /// </summary>
        [HttpGet("test-connection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> TestConnection()
        {
            _logger.LogInformation("Testing HubHop API connection");

            try
            {
                var isConnected = await _hubHopService.TestConnectionAsync();
                
                if (isConnected)
                {
                    _logger.LogInformation("HubHop API connection test successful");
                    return Ok(new { success = true, message = "HubHop API is accessible" });
                }
                else
                {
                    _logger.LogWarning("HubHop API connection test failed");
                    return StatusCode(503, new { success = false, message = "HubHop API is not accessible" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error testing HubHop API connection");
                return StatusCode(503, new { success = false, message = $"Connection test failed: {ex.Message}" });
            }
        }

        /// <summary>
        /// Fetch all presets from HubHop API
        /// </summary>
        [HttpGet("presets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<HubHopPresetDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetAllPresets()
        {
            _logger.LogInformation("Fetching all HubHop presets");

            try
            {
                var presets = await _hubHopService.GetAllPresetsAsync();
                
                _logger.LogInformation("Retrieved {Count} HubHop presets", presets.Count);
                return Ok(presets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching HubHop presets");
                return StatusCode(500, new ErrorDto { ErrorMessage = "Failed to fetch presets", Details = new Dictionary<string, object> { { "exception", ex.Message } } });
            }
        }

        /// <summary>
        /// Fetch presets filtered by vendor
        /// </summary>
        [HttpGet("presets/vendor/{vendor}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<HubHopPresetDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetPresetsByVendor(string vendor)
        {
            _logger.LogInformation("Fetching HubHop presets for vendor: {Vendor}", vendor);

            try
            {
                var presets = await _hubHopService.GetPresetsByVendorAsync(vendor);
                
                _logger.LogInformation("Retrieved {Count} HubHop presets for vendor {Vendor}", presets.Count, vendor);
                return Ok(presets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching HubHop presets for vendor {Vendor}", vendor);
                return StatusCode(500, new ErrorDto { ErrorMessage = $"Failed to fetch presets for vendor {vendor}", Details = new Dictionary<string, object> { { "exception", ex.Message } } });
            }
        }

        /// <summary>
        /// Fetch presets filtered by aircraft
        /// </summary>
        [HttpGet("presets/aircraft/{aircraft}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<HubHopPresetDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetPresetsByAircraft(string aircraft)
        {
            _logger.LogInformation("Fetching HubHop presets for aircraft: {Aircraft}", aircraft);

            try
            {
                var presets = await _hubHopService.GetPresetsByAircraftAsync(aircraft);
                
                _logger.LogInformation("Retrieved {Count} HubHop presets for aircraft {Aircraft}", presets.Count, aircraft);
                return Ok(presets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching HubHop presets for aircraft {Aircraft}", aircraft);
                return StatusCode(500, new ErrorDto { ErrorMessage = $"Failed to fetch presets for aircraft {aircraft}", Details = new Dictionary<string, object> { { "exception", ex.Message } } });
            }
        }

        /// <summary>
        /// Convert HubHop presets to SimulatorEvent DTOs
        /// </summary>
        [HttpGet("presets/as-simulator-events")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<SimulatorEventDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetPresetsAsSimulatorEvents()
        {
            _logger.LogInformation("Converting HubHop presets to SimulatorEvent DTOs");

            try
            {
                var simulatorEvents = await _hubHopService.GetPresetsAsSimulatorEventsAsync();
                
                _logger.LogInformation("Converted {Count} HubHop presets to SimulatorEvents", simulatorEvents.Count);
                return Ok(simulatorEvents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting HubHop presets to SimulatorEvents");
                return StatusCode(500, new ErrorDto { ErrorMessage = "Failed to convert presets to SimulatorEvents", Details = new Dictionary<string, object> { { "exception", ex.Message } } });
            }
        }

        /// <summary>
        /// Synchronize HubHop presets with local SimulatorEvent database
        /// </summary>
        [HttpPost("sync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HubHopSyncResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> SynchronizePresets()
        {
            _logger.LogInformation("Starting HubHop presets synchronization");

            try
            {
                var result = await _hubHopService.SynchronizePresetsAsync();
                
                if (result.Success)
                {
                    _logger.LogInformation("HubHop synchronization completed successfully: {Details}", result.Details);
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("HubHop synchronization failed: {ErrorMessage}", result.ErrorMessage);
                    return StatusCode(500, new ErrorDto { ErrorMessage = "Synchronization failed", Details = new Dictionary<string, object> { { "error", result.ErrorMessage } } });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during HubHop synchronization");
                return StatusCode(500, new ErrorDto { ErrorMessage = "Synchronization error", Details = new Dictionary<string, object> { { "exception", ex.Message } } });
            }
        }

        /// <summary>
        /// Get synchronization statistics and information
        /// </summary>
        [HttpGet("sync/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSyncInfo()
        {
            _logger.LogInformation("Getting HubHop synchronization information");

            try
            {
                var syncInfo = new
                {
                    description = "HubHop FS2020 presets synchronization service",
                    endpoints = new
                    {
                        testConnection = "/hubhop/test-connection",
                        getAllPresets = "/hubhop/presets",
                        getByVendor = "/hubhop/presets/vendor/{vendor}",
                        getByAircraft = "/hubhop/presets/aircraft/{aircraft}",
                        getAsSimulatorEvents = "/hubhop/presets/as-simulator-events",
                        synchronize = "POST /hubhop/sync"
                    },
                    features = new[]
                    {
                        "Fetch FS2020 presets from HubHop community database",
                        "Filter presets by vendor or aircraft",
                        "Convert presets to OpenA3XX SimulatorEvent format",
                        "Synchronize with local database (add new, update existing)",
                        "Comprehensive logging and error handling"
                    }
                };

                return Ok(syncInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sync info");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
} 