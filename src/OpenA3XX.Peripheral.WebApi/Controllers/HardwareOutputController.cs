using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services.Hardware;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing hardware outputs in the OpenA3XX system.
    /// Provides CRUD operations for hardware output configurations used in cockpit panels.
    /// </summary>
    [ApiController]
    [Route("hardware-outputs")]
    [Produces("application/json")]
    public class HardwareOutputController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareOutputService _hardwareOutputService;
        private readonly ILogger<HardwareOutputController> _logger;

        /// <summary>
        /// Initializes a new instance of the HardwareOutputController
        /// </summary>
        /// <param name="logger">Logger instance for this controller</param>
        /// <param name="accessor">HTTP context accessor for request information</param>
        /// <param name="hardwareOutputService">Service for hardware output operations</param>
        public HardwareOutputController(ILogger<HardwareOutputController> logger, IHttpContextAccessor accessor,
            IHardwareOutputService hardwareOutputService)
        {
            _logger = logger;
            _accessor = accessor;
            _hardwareOutputService = hardwareOutputService;
        }

        /// <summary>
        /// Retrieves all hardware outputs or filters by panel ID
        /// </summary>
        /// <param name="panelId">Optional panel ID to filter hardware outputs</param>
        /// <returns>A list of hardware outputs</returns>
        /// <response code="200">Returns the list of hardware outputs</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<HardwareOutputDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IList<HardwareOutputDto> GetAll([FromQuery] int? panelId = null)
        {
            _logger.LogInformation("API Request: Getting hardware outputs from {ClientIP} with panel filter: {PanelId}", 
                _accessor.HttpContext?.Connection?.RemoteIpAddress, panelId);

            IList<HardwareOutputDto> data;
            if (panelId.HasValue)
            {
                data = _hardwareOutputService.GetByPanelId(panelId.Value);
                _logger.LogInformation("API Response: Returning {Count} hardware outputs for panel {PanelId}", data.Count, panelId.Value);
            }
            else
            {
                data = _hardwareOutputService.GetAll();
                _logger.LogInformation("API Response: Returning {Count} hardware outputs", data.Count);
            }

            return data;
        }

        /// <summary>
        /// Retrieves a specific hardware output by its ID
        /// </summary>
        /// <param name="hardwareOutputId">The unique identifier of the hardware output</param>
        /// <returns>The hardware output with the specified ID</returns>
        /// <response code="200">Returns the requested hardware output</response>
        /// <response code="404">If the hardware output is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{hardwareOutputId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareOutputDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public HardwareOutputDto GetById(int hardwareOutputId)
        {
            _logger.LogInformation("API Request: Getting hardware output with ID {Id} from {ClientIP}", 
                hardwareOutputId, _accessor.HttpContext?.Connection?.RemoteIpAddress);

            var data = _hardwareOutputService.GetBy(hardwareOutputId);
            
            if (data == null)
            {
                _logger.LogWarning("Hardware output with ID {Id} not found", hardwareOutputId);
                throw new EntityNotFoundException("HardwareOutput", hardwareOutputId);
            }
            
            _logger.LogInformation("API Response: Returning hardware output '{Name}' (ID: {Id})", data.Name, data.Id);
            
            return data;
        }

        /// <summary>
        /// Creates a new hardware output in the system
        /// </summary>
        /// <param name="hardwareOutputDto">The hardware output data to create</param>
        /// <returns>The created hardware output with assigned ID</returns>
        /// <response code="200">Returns the created hardware output</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareOutputDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult AddHardwareOutput([FromBody] HardwareOutputDto hardwareOutputDto)
        {
            if (hardwareOutputDto == null)
            {
                _logger.LogWarning("Attempted to create hardware output with null data");
                return BadRequest(ErrorDto.Create("Hardware output data is required", "INVALID_INPUT"));
            }

            _logger.LogInformation("API Request: Creating hardware output '{Name}' for panel {PanelId} from {ClientIP}", 
                hardwareOutputDto.Name, hardwareOutputDto.HardwarePanelId, _accessor.HttpContext?.Connection?.RemoteIpAddress);

            try
            {
                var result = _hardwareOutputService.Add(hardwareOutputDto);
                
                _logger.LogInformation("API Response: Successfully created hardware output '{Name}' (ID: {Id}) for panel {PanelId}", 
                    result.Name, result.Id, result.HardwarePanelId);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create hardware output '{Name}' for panel {PanelId}", 
                    hardwareOutputDto.Name, hardwareOutputDto.HardwarePanelId);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing hardware output
        /// </summary>
        /// <param name="hardwareOutputDto">The hardware output data to update (must include ID)</param>
        /// <returns>The updated hardware output</returns>
        /// <response code="200">Returns the updated hardware output</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="404">If the hardware output to update is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareOutputDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public HardwareOutputDto UpdateHardwareOutput([FromBody] HardwareOutputDto hardwareOutputDto)
        {
            if (hardwareOutputDto?.Id == null)
            {
                _logger.LogWarning("Attempted to update hardware output with invalid data");
                throw new ValidationException("Id", "Hardware output ID is required for updates");
            }

            _logger.LogInformation("API Request: Updating hardware output '{Name}' (ID: {Id}) for panel {PanelId} from {ClientIP}", 
                hardwareOutputDto.Name, hardwareOutputDto.Id, hardwareOutputDto.HardwarePanelId, _accessor.HttpContext?.Connection?.RemoteIpAddress);

            try
            {
                var result = _hardwareOutputService.Update(hardwareOutputDto);
                
                _logger.LogInformation("API Response: Successfully updated hardware output '{Name}' (ID: {Id}) for panel {PanelId}", 
                    result.Name, result.Id, result.HardwarePanelId);
                
                return result;
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware output with ID {Id} not found for update", hardwareOutputDto.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update hardware output '{Name}' (ID: {Id}) for panel {PanelId}", 
                    hardwareOutputDto.Name, hardwareOutputDto.Id, hardwareOutputDto.HardwarePanelId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a hardware output from the system
        /// </summary>
        /// <param name="hardwareOutputId">The unique identifier of the hardware output to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the hardware output was successfully deleted</response>
        /// <response code="404">If the hardware output is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{hardwareOutputId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult DeleteHardwareOutput(int hardwareOutputId)
        {
            _logger.LogInformation("API Request: Deleting hardware output with ID {Id} from {ClientIP}", 
                hardwareOutputId, _accessor.HttpContext?.Connection?.RemoteIpAddress);

            try
            {
                _hardwareOutputService.Delete(hardwareOutputId);
                
                _logger.LogInformation("API Response: Successfully deleted hardware output with ID {Id}", hardwareOutputId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware output with ID {Id} not found for deletion", hardwareOutputId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hardware output with ID {Id}", hardwareOutputId);
                throw;
            }
        }
    }
} 