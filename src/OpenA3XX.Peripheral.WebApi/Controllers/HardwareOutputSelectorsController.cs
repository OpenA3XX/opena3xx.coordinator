using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services;
using System;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-output-selectors")]
    [Produces("application/json")]
    public class HardwareOutputSelectorsController : ControllerBase
    {
        private readonly IHardwareOutputSelectorService _hardwareOutputSelectorService;
        private readonly ILogger<HardwareOutputSelectorsController> _logger;

        public HardwareOutputSelectorsController(ILogger<HardwareOutputSelectorsController> logger,
            IHardwareOutputSelectorService hardwareOutputSelectorService
        )
        {
            _logger = logger;
            _hardwareOutputSelectorService = hardwareOutputSelectorService;
        }

        /// <summary>
        /// Retrieves a specific hardware output selector by its ID
        /// </summary>
        /// <param name="hardwareOutputSelectorId">The unique identifier of the hardware output selector</param>
        /// <returns>The hardware output selector with the specified ID</returns>
        /// <response code="200">Returns the requested hardware output selector</response>
        /// <response code="404">If the hardware output selector is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{hardwareOutputSelectorId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareOutputSelectorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public HardwareOutputSelectorDto GetHardwareOutputSelectorDetails(int hardwareOutputSelectorId)
        {
            _logger.LogInformation("API Request: Getting hardware output selector details for ID: {Id}", hardwareOutputSelectorId);
            
            var data = _hardwareOutputSelectorService.GetHardwareOutputSelectorDetails(hardwareOutputSelectorId);
            
            _logger.LogInformation("API Response: Returning hardware output selector {Name} (ID: {Id})", data.Name, data.Id);
            
            return data;
        }

        /// <summary>
        /// Creates a new hardware output selector in the system
        /// </summary>
        /// <param name="addHardwareOutputSelectorDto">The hardware output selector data to create</param>
        /// <returns>The created hardware output selector with assigned ID</returns>
        /// <response code="200">Returns the created hardware output selector</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareOutputSelectorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult AddHardwareOutputSelector([FromBody] AddHardwareOutputSelectorDto addHardwareOutputSelectorDto)
        {
            _logger.LogInformation("API Request: Creating new hardware output selector '{Name}' for hardware output {HardwareOutputId}", 
                addHardwareOutputSelectorDto.Name, addHardwareOutputSelectorDto.HardwareOutputId);

            try
            {
                var result = _hardwareOutputSelectorService.Add(addHardwareOutputSelectorDto);
                
                _logger.LogInformation("API Response: Successfully created hardware output selector '{Name}' (ID: {Id}) for hardware output {HardwareOutputId}", 
                    result.Name, result.Id, result.HardwareOutputId);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create hardware output selector '{Name}' for hardware output {HardwareOutputId}", 
                    addHardwareOutputSelectorDto.Name, addHardwareOutputSelectorDto.HardwareOutputId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a hardware output selector from the system
        /// </summary>
        /// <param name="hardwareOutputSelectorId">The unique identifier of the hardware output selector to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the hardware output selector was successfully deleted</response>
        /// <response code="404">If the hardware output selector is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{hardwareOutputSelectorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult DeleteHardwareOutputSelector(int hardwareOutputSelectorId)
        {
            _logger.LogInformation("API Request: Deleting hardware output selector with ID {Id}", hardwareOutputSelectorId);

            try
            {
                _hardwareOutputSelectorService.Delete(hardwareOutputSelectorId);
                
                _logger.LogInformation("API Response: Successfully deleted hardware output selector with ID {Id}", hardwareOutputSelectorId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware output selector with ID {Id} not found for deletion", hardwareOutputSelectorId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hardware output selector with ID {Id}", hardwareOutputSelectorId);
                throw;
            }
        }
    }
}