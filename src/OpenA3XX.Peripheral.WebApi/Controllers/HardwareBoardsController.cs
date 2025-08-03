using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-boards")]
    [Produces("application/json")]
    public class HardwareBoardsController : ControllerBase
    {
        private readonly ILogger<HardwareBoardsController> _logger;
        private readonly IHardwareBoardService _hardwareBoardService;
        private readonly IHardwareInputSelectorService _hardwareInputSelectorService;

        public HardwareBoardsController(ILogger<HardwareBoardsController> logger, IHardwareBoardService hardwareBoardService, IHardwareInputSelectorService hardwareInputSelectorService)
        {
            _logger = logger;
            _hardwareBoardService = hardwareBoardService;
            _hardwareInputSelectorService = hardwareInputSelectorService;
        }

        [HttpGet("all")]
        public IList<HardwareBoardDto> GetAllHardwareBoards()
        {
            var hardwareBoardDtos = _hardwareBoardService.GetAllHardwareBoards();
            return hardwareBoardDtos;
        }

        [HttpGet("{hardwareBoardId}")]
        public HardwareBoardDetailsDto GetHardwareBoardDetails(int hardwareBoardId)
        {
            var hardwareBoardDetailsDto = _hardwareBoardService.GetHardwareBoard(hardwareBoardId);
            return hardwareBoardDetailsDto;
        }
        
        [HttpPost("add")]
        public HardwareBoardDto RegisterHardwareBoard([FromBody] HardwareBoardDto hardwareBoardDto)
        {
            var data = _hardwareBoardService.SaveHardwareBoard(hardwareBoardDto);
            return data;
        }

        /// <summary>
        /// Updates an existing hardware board
        /// </summary>
        /// <param name="hardwareBoardId">The unique identifier of the hardware board to update</param>
        /// <param name="hardwareBoardDto">The hardware board data to update</param>
        /// <returns>The updated hardware board</returns>
        /// <response code="200">Returns the updated hardware board</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="404">If the hardware board is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPatch("{hardwareBoardId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareBoardDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult UpdateHardwareBoard(int hardwareBoardId, [FromBody] HardwareBoardDto hardwareBoardDto)
        {
            if (hardwareBoardDto == null)
            {
                _logger.LogWarning("Attempted to update hardware board with null data");
                return BadRequest(ErrorDto.Create("Hardware board data is required", "INVALID_INPUT"));
            }

            // Ensure the ID in the path matches the ID in the body
            if (hardwareBoardDto.Id != hardwareBoardId)
            {
                _logger.LogWarning("Hardware board ID mismatch: path={PathId}, body={BodyId}", hardwareBoardId, hardwareBoardDto.Id);
                return BadRequest(ErrorDto.Create("Hardware board ID in path must match ID in body", "ID_MISMATCH"));
            }

            _logger.LogInformation("API Request: Updating hardware board '{Name}' (ID: {Id}) with {BusCount} buses", 
                hardwareBoardDto.Name, hardwareBoardDto.Id, hardwareBoardDto.HardwareBusExtendersCount);

            try
            {
                var result = _hardwareBoardService.Update(hardwareBoardDto);
                
                _logger.LogInformation("API Response: Successfully updated hardware board '{Name}' (ID: {Id}) with {BusCount} buses", 
                    result.Name, result.Id, result.HardwareBusExtendersCount);
                
                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware board with ID {Id} not found for update", hardwareBoardId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update hardware board '{Name}' (ID: {Id})", 
                    hardwareBoardDto.Name, hardwareBoardDto.Id);
                throw;
            }
        }

        [HttpPost("link/hardware-input-selector")]
        public HardwareBoardDetailsDto LinkExtenderBitToHardwareInputSelector([FromBody] MapExtenderBitToHardwareInputSelectorDto linkExtenderBitToHardwareInputSelectorDto)
        {
            var hardwareBoardDetailsDto = _hardwareBoardService.LinkExtenderBitToHardwareInputSelector(linkExtenderBitToHardwareInputSelectorDto);
            return hardwareBoardDetailsDto;
        }
        
        [HttpPost("link/hardware-output-selector")]
        public HardwareBoardDetailsDto LinkExtenderBitToHardwareOutputSelector([FromBody] MapExtenderBitToHardwareOutputSelectorDto linkExtenderBitToHardwareOutputSelectorDto)
        {
            var hardwareBoardDetailsDto = _hardwareBoardService.LinkExtenderBitToHardwareOutputSelector(linkExtenderBitToHardwareOutputSelectorDto);
            return hardwareBoardDetailsDto;
        }
        

        [HttpGet("hardware-input-selector/{hardwareInputSelectorId}")]
        public MapExtenderBitToHardwareInputSelectorDto GetHardwareBoardAssociationForHardwareInputSelector(int hardwareInputSelectorId)
        {
            var linkExtenderBitToHardwareInputSelectorDto = _hardwareBoardService.GetHardwareBoardAssociationForHardwareInputSelector(hardwareInputSelectorId);
            return linkExtenderBitToHardwareInputSelectorDto;
        }

        /// <summary>
        /// Unmaps a hardware input selector from its current hardware board association
        /// </summary>
        /// <param name="hardwareInputSelectorId">The ID of the hardware input selector to unmap</param>
        /// <returns>No content on successful unmapping</returns>
        /// <response code="204">If the hardware input selector was successfully unmapped</response>
        /// <response code="404">If the hardware input selector is not found or not mapped</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("unmap/hardware-input-selector/{hardwareInputSelectorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult UnmapHardwareInputSelector(int hardwareInputSelectorId)
        {
            _logger.LogInformation("API Request: Unmapping hardware input selector with ID {Id}", hardwareInputSelectorId);

            try
            {
                _hardwareBoardService.UnmapHardwareInputSelector(hardwareInputSelectorId);
                
                _logger.LogInformation("API Response: Successfully unmapped hardware input selector with ID {Id}", hardwareInputSelectorId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware input selector with ID {Id} not found for unmapping", hardwareInputSelectorId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to unmap hardware input selector with ID {Id}", hardwareInputSelectorId);
                throw;
            }
        }
        
        [HttpGet("hardware-output-selector/{hardwareOutputSelectorId}")]
        public MapExtenderBitToHardwareOutputSelectorDto GetHardwareBoardAssociationForHardwareOutputSelector(int hardwareOutputSelectorId)
        {
            var linkExtenderBitToHardwareOutputSelectorDto = _hardwareBoardService.GetHardwareBoardAssociationForHardwareOutputSelector(hardwareOutputSelectorId);
            return linkExtenderBitToHardwareOutputSelectorDto;
        }

        /// <summary>
        /// Unmaps a hardware output selector from its current hardware board association
        /// </summary>
        /// <param name="hardwareOutputSelectorId">The ID of the hardware output selector to unmap</param>
        /// <returns>No content on successful unmapping</returns>
        /// <response code="204">If the hardware output selector was successfully unmapped</response>
        /// <response code="404">If the hardware output selector is not found or not mapped</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("unmap/hardware-output-selector/{hardwareOutputSelectorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult UnmapHardwareOutputSelector(int hardwareOutputSelectorId)
        {
            _logger.LogInformation("API Request: Unmapping hardware output selector with ID {Id}", hardwareOutputSelectorId);

            try
            {
                _hardwareBoardService.UnmapHardwareOutputSelector(hardwareOutputSelectorId);
                
                _logger.LogInformation("API Response: Successfully unmapped hardware output selector with ID {Id}", hardwareOutputSelectorId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware output selector with ID {Id} not found for unmapping", hardwareOutputSelectorId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to unmap hardware output selector with ID {Id}", hardwareOutputSelectorId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a hardware board and all its associated entities from the system
        /// </summary>
        /// <param name="hardwareBoardId">The unique identifier of the hardware board to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the hardware board was successfully deleted</response>
        /// <response code="404">If the hardware board is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{hardwareBoardId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult DeleteHardwareBoard(int hardwareBoardId)
        {
            _logger.LogInformation("API Request: Deleting hardware board with ID {Id} and all associated entities", hardwareBoardId);

            try
            {
                _hardwareBoardService.Delete(hardwareBoardId);
                
                _logger.LogInformation("API Response: Successfully deleted hardware board with ID {Id} and all associated entities", hardwareBoardId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware board with ID {Id} not found for deletion", hardwareBoardId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hardware board with ID {Id}", hardwareBoardId);
                throw;
            }
        }
    }

}