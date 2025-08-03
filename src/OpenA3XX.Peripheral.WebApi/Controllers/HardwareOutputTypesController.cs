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
    [Route("hardware-output-types")]
    [Produces("application/json")]
    public class HardwareOutputTypesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareOutputTypeService _hardwareOutputTypeService;
        private readonly ILogger<HardwareOutputTypesController> _logger;

        public HardwareOutputTypesController(ILogger<HardwareOutputTypesController> logger,
            IHttpContextAccessor accessor, IHardwareOutputTypeService hardwareOutputTypeService)
        {
            _logger = logger;
            _accessor = accessor;
            _hardwareOutputTypeService = hardwareOutputTypeService;
        }

        [HttpGet]
        public IList<HardwareOutputTypeDto> GetAll()
        {
            var data = _hardwareOutputTypeService.GetAll();

            return data;
        }

        [HttpGet("{hardwareOutputTypeId}")]
        public HardwareOutputTypeDto GetById(int hardwareOutputTypeId)
        {
            var data = _hardwareOutputTypeService.GetBy(hardwareOutputTypeId);

            return data;
        }

        [HttpPost]
        public IActionResult AddHardwareOutputType([FromBody] HardwareOutputTypeDto hardwareOutputTypeDto)
        {
            try
            {
                hardwareOutputTypeDto = _hardwareOutputTypeService.Add(hardwareOutputTypeDto);
                return Ok(hardwareOutputTypeDto);
            }
            catch (HardwareOutputTypeExistsException e)
            {
                return BadRequest(new ErrorDto
                {
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPatch]
        public HardwareOutputTypeDto UpdateHardwareOutputType([FromBody] HardwareOutputTypeDto hardwareOutputTypeDto)
        {
            hardwareOutputTypeDto = _hardwareOutputTypeService.Update(hardwareOutputTypeDto);
            return hardwareOutputTypeDto;
        }

        /// <summary>
        /// Deletes a hardware output type from the system
        /// </summary>
        /// <param name="hardwareOutputTypeId">The unique identifier of the hardware output type to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the hardware output type was successfully deleted</response>
        /// <response code="404">If the hardware output type is not found</response>
        /// <response code="400">If the hardware output type is being used by hardware outputs</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{hardwareOutputTypeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult DeleteHardwareOutputType(int hardwareOutputTypeId)
        {
            _logger.LogInformation("API Request: Deleting hardware output type with ID {Id}", hardwareOutputTypeId);

            try
            {
                _hardwareOutputTypeService.Delete(hardwareOutputTypeId);
                
                _logger.LogInformation("API Response: Successfully deleted hardware output type with ID {Id}", hardwareOutputTypeId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware output type with ID {Id} not found for deletion", hardwareOutputTypeId);
                throw;
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Cannot delete hardware output type with ID {Id}: {Message}", hardwareOutputTypeId, ex.Message);
                return BadRequest(ErrorDto.Create(ex.Message, "HARDWARE_OUTPUT_TYPE_IN_USE"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hardware output type with ID {Id}", hardwareOutputTypeId);
                throw;
            }
        }
    }
}