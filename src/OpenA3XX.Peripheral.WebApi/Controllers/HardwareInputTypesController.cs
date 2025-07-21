using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing hardware input types in the OpenA3XX system.
    /// Provides CRUD operations for hardware input type configurations used in cockpit panels.
    /// </summary>
    [ApiController]
    [Route("hardware-input-types")]
    [Produces("application/json")]
    public class HardwareInputTypesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareInputTypeService _hardwareInputTypeService;
        private readonly ILogger<HardwareInputTypesController> _logger;

        /// <summary>
        /// Initializes a new instance of the HardwareInputTypesController
        /// </summary>
        /// <param name="logger">Logger instance for this controller</param>
        /// <param name="accessor">HTTP context accessor for request information</param>
        /// <param name="hardwareInputTypeService">Service for hardware input type operations</param>
        public HardwareInputTypesController(ILogger<HardwareInputTypesController> logger, IHttpContextAccessor accessor,
            IHardwareInputTypeService hardwareInputTypeService)
        {
            _logger = logger;
            _accessor = accessor;
            _hardwareInputTypeService = hardwareInputTypeService;
        }

        /// <summary>
        /// Retrieves all hardware input types configured in the system
        /// </summary>
        /// <returns>A list of all hardware input types</returns>
        /// <response code="200">Returns the list of hardware input types</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<HardwareInputTypeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IList<HardwareInputTypeDto> GetAll()
        {
            _logger.LogDebug("Retrieving all hardware input types");
            var data = _hardwareInputTypeService.GetAll();
            _logger.LogDebug("Retrieved {Count} hardware input types", data.Count);
            return data;
        }

        /// <summary>
        /// Retrieves a specific hardware input type by its ID
        /// </summary>
        /// <param name="hardwareInputTypeId">The unique identifier of the hardware input type</param>
        /// <returns>The hardware input type with the specified ID</returns>
        /// <response code="200">Returns the requested hardware input type</response>
        /// <response code="404">If the hardware input type is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{hardwareInputTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareInputTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public HardwareInputTypeDto GetById(int hardwareInputTypeId)
        {
            _logger.LogDebug("Retrieving hardware input type with ID: {Id}", hardwareInputTypeId);
            var data = _hardwareInputTypeService.GetBy(hardwareInputTypeId);
            
            if (data == null)
            {
                _logger.LogWarning("Hardware input type with ID {Id} not found", hardwareInputTypeId);
                throw new EntityNotFoundException("HardwareInputType", hardwareInputTypeId);
            }
            
            return data;
        }

        /// <summary>
        /// Creates a new hardware input type in the system
        /// </summary>
        /// <param name="hardwareInputTypeDto">The hardware input type data to create</param>
        /// <returns>The created hardware input type with assigned ID</returns>
        /// <response code="200">Returns the created hardware input type</response>
        /// <response code="400">If the input data is invalid or a hardware input type with the same name already exists</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareInputTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult AddHardwareInputType([FromBody] HardwareInputTypeDto hardwareInputTypeDto)
        {
            if (hardwareInputTypeDto == null)
            {
                _logger.LogWarning("Attempted to create hardware input type with null data");
                return BadRequest(ErrorDto.Create("Hardware input type data is required", "INVALID_INPUT"));
            }

            _logger.LogInformation("Creating hardware input type: {Name}", hardwareInputTypeDto.Name);
            
            try
            {
                hardwareInputTypeDto = _hardwareInputTypeService.Add(hardwareInputTypeDto);
                _logger.LogInformation("Successfully created hardware input type with ID: {Id}", hardwareInputTypeDto.Id);
                return Ok(hardwareInputTypeDto);
            }
            catch (HardwareInputTypeExistsException e)
            {
                _logger.LogWarning(e, "Failed to create hardware input type - already exists: {Name}", hardwareInputTypeDto.Name);
                return BadRequest(ErrorDto.Create(e.Message, "HARDWARE_INPUT_TYPE_EXISTS"));
            }
        }

        /// <summary>
        /// Updates an existing hardware input type
        /// </summary>
        /// <param name="hardwareInputTypeDto">The hardware input type data to update (must include ID)</param>
        /// <returns>The updated hardware input type</returns>
        /// <response code="200">Returns the updated hardware input type</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="404">If the hardware input type to update is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HardwareInputTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public HardwareInputTypeDto UpdateHardwareInputType([FromBody] HardwareInputTypeDto hardwareInputTypeDto)
        {
            if (hardwareInputTypeDto?.Id == null)
            {
                _logger.LogWarning("Attempted to update hardware input type with invalid data");
                throw new ValidationException("Id", "Hardware input type ID is required for updates");
            }

            _logger.LogInformation("Updating hardware input type with ID: {Id}", hardwareInputTypeDto.Id);
            hardwareInputTypeDto = _hardwareInputTypeService.Update(hardwareInputTypeDto);
            _logger.LogInformation("Successfully updated hardware input type with ID: {Id}", hardwareInputTypeDto.Id);
            
            return hardwareInputTypeDto;
        }
    }
}