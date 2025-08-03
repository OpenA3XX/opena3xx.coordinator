using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services;

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