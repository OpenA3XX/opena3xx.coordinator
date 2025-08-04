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
    [ApiController]
    [Route("hardware-panel")]
    [Produces("application/json")]
    public class HardwarePanelController : ControllerBase
    {
        private readonly IHardwarePanelService _hardwarePanelService;
        private readonly ILogger<HardwarePanelController> _logger;

        public HardwarePanelController(ILogger<HardwarePanelController> logger, IHttpContextAccessor accessor,
            IHardwarePanelService hardwarePanelTokensService)
        {
            _logger = logger;
            _hardwarePanelService = hardwarePanelTokensService;
        }

        [HttpPost("add")]
        public HardwarePanelDto AddHardwarePanel(AddHardwarePanelDto hardwarePanelDto)
        {
            return _hardwarePanelService.AddHardwarePanel(hardwarePanelDto);
        }

        [HttpGet("overview/all")]
        public IList<HardwarePanelOverviewDto> GetAllHardwarePanels()
        {
            var data = _hardwarePanelService.GetAllHardwarePanels();
            return data;
        }

        [HttpGet("details/{hardwarePanelId}")]
        public HardwarePanelDto GetHardwarePanelDetails(int hardwarePanelId)
        {
            var data = _hardwarePanelService.GetHardwarePanelDetails(hardwarePanelId);
            return data;
        }

        /// <summary>
        /// Updates an existing hardware panel
        /// </summary>
        /// <param name="hardwarePanelId">The unique identifier of the hardware panel to update</param>
        /// <param name="updateHardwarePanelDto">The hardware panel data to update</param>
        /// <returns>The updated hardware panel</returns>
        /// <response code="200">Returns the updated hardware panel</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="404">If the hardware panel is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPatch("{hardwarePanelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwarePanelDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult UpdateHardwarePanel(int hardwarePanelId, [FromBody] UpdateHardwarePanelDto updateHardwarePanelDto)
        {
            if (updateHardwarePanelDto == null)
            {
                _logger.LogWarning("Attempted to update hardware panel with null data");
                return BadRequest(ErrorDto.Create("Hardware panel data is required", "INVALID_INPUT"));
            }

            _logger.LogInformation("API Request: Updating hardware panel '{Name}' (ID: {Id}) with aircraft model {AircraftModel}, cockpit area {CockpitArea}, owner {Owner}", 
                updateHardwarePanelDto.Name, hardwarePanelId, updateHardwarePanelDto.AircraftModel, updateHardwarePanelDto.CockpitArea, updateHardwarePanelDto.Owner);

            try
            {
                var result = _hardwarePanelService.Update(hardwarePanelId, updateHardwarePanelDto);
                
                _logger.LogInformation("API Response: Successfully updated hardware panel '{Name}' (ID: {Id})", 
                    result.Name, result.Id);
                
                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware panel with ID {Id} not found for update", hardwarePanelId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update hardware panel '{Name}' (ID: {Id})", 
                    updateHardwarePanelDto.Name, hardwarePanelId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a hardware panel and all its associated entities from the system
        /// </summary>
        /// <param name="hardwarePanelId">The unique identifier of the hardware panel to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the hardware panel was successfully deleted</response>
        /// <response code="404">If the hardware panel is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{hardwarePanelId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult DeleteHardwarePanel(int hardwarePanelId)
        {
            _logger.LogInformation("API Request: Deleting hardware panel with ID {Id} and all associated entities", hardwarePanelId);

            try
            {
                _hardwarePanelService.Delete(hardwarePanelId);
                
                _logger.LogInformation("API Response: Successfully deleted hardware panel with ID {Id} and all associated entities", hardwarePanelId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware panel with ID {Id} not found for deletion", hardwarePanelId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hardware panel with ID {Id}", hardwarePanelId);
                throw;
            }
        }
    }
}