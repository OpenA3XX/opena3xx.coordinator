using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services.Hardware;
using System;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing hardware inputs in the OpenA3XX system.
    /// Provides CRUD operations for hardware input configurations used in cockpit panels.
    /// </summary>
    [ApiController]
    [Route("hardware-inputs")]
    [Produces("application/json")]
    public class HardwareInputController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareInputService _hardwareInputService;
        private readonly ILogger<HardwareInputController> _logger;

        /// <summary>
        /// Initializes a new instance of the HardwareInputController
        /// </summary>
        /// <param name="logger">Logger instance for this controller</param>
        /// <param name="accessor">HTTP context accessor for request information</param>
        /// <param name="hardwareInputService">Service for hardware input operations</param>
        public HardwareInputController(ILogger<HardwareInputController> logger, IHttpContextAccessor accessor,
            IHardwareInputService hardwareInputService)
        {
            _logger = logger;
            _accessor = accessor;
            _hardwareInputService = hardwareInputService;
        }

        /// <summary>
        /// Retrieves all hardware inputs or filters by panel ID
        /// </summary>
        /// <param name="panelId">Optional panel ID to filter hardware inputs</param>
        /// <returns>A list of hardware inputs</returns>
        /// <response code="200">Returns the list of hardware inputs</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<HardwareInputDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IList<HardwareInputDto> GetAll([FromQuery] int? panelId = null)
        {
            _logger.LogInformation("API Request: Getting hardware inputs from {ClientIP} - PanelId: {PanelId}", 
                _accessor.HttpContext?.Connection?.RemoteIpAddress, panelId);
                
            IList<HardwareInputDto> data;
            
            if (panelId.HasValue)
            {
                data = _hardwareInputService.GetByPanelId(panelId.Value);
                _logger.LogInformation("API Response: Returning {Count} hardware inputs for panel {PanelId}", data.Count, panelId.Value);
            }
            else
            {
                data = _hardwareInputService.GetAll();
                _logger.LogInformation("API Response: Returning {Count} hardware inputs", data.Count);
            }
            
            return data;
        }

        /// <summary>
        /// Retrieves a specific hardware input by its ID
        /// </summary>
        /// <param name="hardwareInputId">The unique identifier of the hardware input</param>
        /// <returns>The hardware input with the specified ID</returns>
        /// <response code="200">Returns the requested hardware input</response>
        /// <response code="404">If the hardware input is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{hardwareInputId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareInputDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public HardwareInputDto GetById(int hardwareInputId)
        {
            _logger.LogDebug("Retrieving hardware input with ID: {Id}", hardwareInputId);
            
            try
            {
                var data = _hardwareInputService.GetBy(hardwareInputId);
                _logger.LogInformation("API Response: Returning hardware input {Name} (ID: {Id})", data.Name, hardwareInputId);
                return data;
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware input with ID {Id} not found", hardwareInputId);
                throw;
            }
        }

        /// <summary>
        /// Creates a new hardware input in the system
        /// </summary>
        /// <param name="hardwareInputDto">The hardware input data to create</param>
        /// <returns>The created hardware input with assigned ID</returns>
        /// <response code="200">Returns the created hardware input</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareInputDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult AddHardwareInput([FromBody] HardwareInputDto hardwareInputDto)
        {
            _logger.LogInformation("API Request: Creating new hardware input '{Name}' for panel {PanelId} from {ClientIP}", 
                hardwareInputDto.Name, hardwareInputDto.HardwarePanelId, _accessor.HttpContext?.Connection?.RemoteIpAddress);

            try
            {
                var result = _hardwareInputService.Add(hardwareInputDto);
                
                _logger.LogInformation("API Response: Successfully created hardware input '{Name}' (ID: {Id}) for panel {PanelId}", 
                    result.Name, result.Id, result.HardwarePanelId);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create hardware input '{Name}' for panel {PanelId}", 
                    hardwareInputDto.Name, hardwareInputDto.HardwarePanelId);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing hardware input in the system
        /// </summary>
        /// <param name="hardwareInputDto">The hardware input data to update</param>
        /// <returns>The updated hardware input</returns>
        /// <response code="200">Returns the updated hardware input</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="404">If the hardware input is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareInputDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public HardwareInputDto UpdateHardwareInput([FromBody] HardwareInputDto hardwareInputDto)
        {
            _logger.LogInformation("API Request: Updating hardware input '{Name}' (ID: {Id}) for panel {PanelId} from {ClientIP}", 
                hardwareInputDto.Name, hardwareInputDto.Id, hardwareInputDto.HardwarePanelId, _accessor.HttpContext?.Connection?.RemoteIpAddress);

            try
            {
                var result = _hardwareInputService.Update(hardwareInputDto);
                
                _logger.LogInformation("API Response: Successfully updated hardware input '{Name}' (ID: {Id}) for panel {PanelId}", 
                    result.Name, result.Id, result.HardwarePanelId);
                
                return result;
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware input with ID {Id} not found for update", hardwareInputDto.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update hardware input '{Name}' (ID: {Id}) for panel {PanelId}", 
                    hardwareInputDto.Name, hardwareInputDto.Id, hardwareInputDto.HardwarePanelId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a hardware input from the system
        /// </summary>
        /// <param name="hardwareInputId">The unique identifier of the hardware input to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the hardware input was successfully deleted</response>
        /// <response code="404">If the hardware input is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{hardwareInputId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult DeleteHardwareInput(int hardwareInputId)
        {
            _logger.LogInformation("API Request: Deleting hardware input with ID {Id} from {ClientIP}", 
                hardwareInputId, _accessor.HttpContext?.Connection?.RemoteIpAddress);

            try
            {
                _hardwareInputService.Delete(hardwareInputId);
                
                _logger.LogInformation("API Response: Successfully deleted hardware input with ID {Id}", hardwareInputId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware input with ID {Id} not found for deletion", hardwareInputId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hardware input with ID {Id}", hardwareInputId);
                throw;
            }
        }
    }
} 