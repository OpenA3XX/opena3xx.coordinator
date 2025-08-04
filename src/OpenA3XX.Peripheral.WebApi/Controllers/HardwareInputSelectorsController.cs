using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services.Hardware;
using System;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-input-selectors")]
    [Produces("application/json")]
    public class HardwareInputSelectorsController : ControllerBase
    {
        private readonly IHardwareInputSelectorService _hardwareInputSelectorService;
        private readonly ILogger<HardwareInputSelectorsController> _logger;

        public HardwareInputSelectorsController(ILogger<HardwareInputSelectorsController> logger,
            IHardwareInputSelectorService hardwareInputSelectorService
        )
        {
            _logger = logger;
            _hardwareInputSelectorService = hardwareInputSelectorService;
        }

        /// <summary>
        /// Retrieves a specific hardware input selector by its ID
        /// </summary>
        /// <param name="hardwareInputSelectorId">The unique identifier of the hardware input selector</param>
        /// <returns>The hardware input selector with the specified ID</returns>
        /// <response code="200">Returns the requested hardware input selector</response>
        /// <response code="404">If the hardware input selector is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{hardwareInputSelectorId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareInputSelectorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public HardwareInputSelectorDto GetHardwareInputSelectorDetails(int hardwareInputSelectorId)
        {
            _logger.LogInformation("API Request: Getting hardware input selector details for ID: {Id}", hardwareInputSelectorId);
            
            var data = _hardwareInputSelectorService.GetHardwareInputSelectorDetails(hardwareInputSelectorId);
            
            _logger.LogInformation("API Response: Returning hardware input selector {Name} (ID: {Id})", data.Name, data.Id);
            
            return data;
        }

        /// <summary>
        /// Creates a new hardware input selector in the system
        /// </summary>
        /// <param name="addHardwareInputSelectorDto">The hardware input selector data to create</param>
        /// <returns>The created hardware input selector with assigned ID</returns>
        /// <response code="200">Returns the created hardware input selector</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareInputSelectorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult AddHardwareInputSelector([FromBody] AddHardwareInputSelectorDto addHardwareInputSelectorDto)
        {
            _logger.LogInformation("API Request: Creating new hardware input selector '{Name}' for hardware input {HardwareInputId}", 
                addHardwareInputSelectorDto.Name, addHardwareInputSelectorDto.HardwareInputId);

            try
            {
                var result = _hardwareInputSelectorService.Add(addHardwareInputSelectorDto);
                
                _logger.LogInformation("API Response: Successfully created hardware input selector '{Name}' (ID: {Id}) for hardware input {HardwareInputId}", 
                    result.Name, result.Id, result.HardwareInputId);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create hardware input selector '{Name}' for hardware input {HardwareInputId}", 
                    addHardwareInputSelectorDto.Name, addHardwareInputSelectorDto.HardwareInputId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a hardware input selector from the system
        /// </summary>
        /// <param name="hardwareInputSelectorId">The unique identifier of the hardware input selector to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the hardware input selector was successfully deleted</response>
        /// <response code="404">If the hardware input selector is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{hardwareInputSelectorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult DeleteHardwareInputSelector(int hardwareInputSelectorId)
        {
            _logger.LogInformation("API Request: Deleting hardware input selector with ID {Id}", hardwareInputSelectorId);

            try
            {
                _hardwareInputSelectorService.Delete(hardwareInputSelectorId);
                
                _logger.LogInformation("API Response: Successfully deleted hardware input selector with ID {Id}", hardwareInputSelectorId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware input selector with ID {Id} not found for deletion", hardwareInputSelectorId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hardware input selector with ID {Id}", hardwareInputSelectorId);
                throw;
            }
        }
    }
}