using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing aircraft models in the OpenA3XX system.
    /// Provides CRUD operations for aircraft model configurations.
    /// </summary>
    [ApiController]
    [Route("aircraft-models")]
    [Produces("application/json")]
    public class AircraftModelController : ControllerBase
    {
        private readonly IAircraftModelService _aircraftModelService;
        private readonly ILogger<AircraftModelController> _logger;

        /// <summary>
        /// Initializes a new instance of the AircraftModelController
        /// </summary>
        /// <param name="logger">Logger instance for this controller</param>
        /// <param name="aircraftModelService">Service for aircraft model operations</param>
        public AircraftModelController(
            ILogger<AircraftModelController> logger,
            IAircraftModelService aircraftModelService)
        {
            _logger = logger;
            _aircraftModelService = aircraftModelService;
        }

        /// <summary>
        /// Retrieves all aircraft models
        /// </summary>
        /// <returns>A list of all aircraft models</returns>
        /// <response code="200">Returns the list of aircraft models</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<AircraftModelDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAircraftModels()
        {
            _logger.LogInformation("API Request: Getting all aircraft models");
            
            var aircraftModels = _aircraftModelService.GetAllAircraftModels();
            
            _logger.LogInformation("API Response: Returning {Count} aircraft models", aircraftModels.Count);
            
            return Ok(aircraftModels);
        }

        /// <summary>
        /// Retrieves all active aircraft models
        /// </summary>
        /// <returns>A list of active aircraft models</returns>
        /// <response code="200">Returns the list of active aircraft models</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<AircraftModelDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetActiveAircraftModels()
        {
            _logger.LogInformation("API Request: Getting active aircraft models");
            
            var aircraftModels = _aircraftModelService.GetActiveAircraftModels();
            
            _logger.LogInformation("API Response: Returning {Count} active aircraft models", aircraftModels.Count);
            
            return Ok(aircraftModels);
        }

        /// <summary>
        /// Retrieves a specific aircraft model by ID
        /// </summary>
        /// <param name="id">The aircraft model ID</param>
        /// <returns>The aircraft model details</returns>
        /// <response code="200">Returns the aircraft model details</response>
        /// <response code="404">If the aircraft model is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AircraftModelDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAircraftModelById(int id)
        {
            _logger.LogInformation("API Request: Getting aircraft model by ID {AircraftModelId}", id);
            
            var aircraftModel = _aircraftModelService.GetAircraftModelById(id);
            
            if (aircraftModel == null)
            {
                _logger.LogWarning("API Response: Aircraft model with ID {AircraftModelId} not found", id);
                return NotFound();
            }
            
            _logger.LogInformation("API Response: Successfully retrieved aircraft model with ID {AircraftModelId}", id);
            
            return Ok(aircraftModel);
        }

        /// <summary>
        /// Creates a new aircraft model
        /// </summary>
        /// <param name="aircraftModelDto">The aircraft model data</param>
        /// <returns>The created aircraft model</returns>
        /// <response code="201">Returns the created aircraft model</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AircraftModelDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddAircraftModel([FromBody] AddAircraftModelDto aircraftModelDto)
        {
            _logger.LogInformation("API Request: Adding new aircraft model: {ModelName}", aircraftModelDto?.Name);
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("API Response: Invalid model state for aircraft model creation");
                return BadRequest(ModelState);
            }
            
            var createdAircraftModel = _aircraftModelService.AddAircraftModel(aircraftModelDto);
            
            _logger.LogInformation("API Response: Successfully created aircraft model with ID {AircraftModelId}", createdAircraftModel.Id);
            
            return CreatedAtAction(nameof(GetAircraftModelById), new { id = createdAircraftModel.Id }, createdAircraftModel);
        }

        /// <summary>
        /// Updates an existing aircraft model
        /// </summary>
        /// <param name="id">The aircraft model ID</param>
        /// <param name="aircraftModelDto">The updated aircraft model data</param>
        /// <returns>The updated aircraft model</returns>
        /// <response code="200">Returns the updated aircraft model</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="404">If the aircraft model is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AircraftModelDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAircraftModel(int id, [FromBody] UpdateAircraftModelDto aircraftModelDto)
        {
            _logger.LogInformation("API Request: Updating aircraft model with ID {AircraftModelId}", id);
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("API Response: Invalid model state for aircraft model update");
                return BadRequest(ModelState);
            }
            
            var updatedAircraftModel = _aircraftModelService.UpdateAircraftModel(id, aircraftModelDto);
            
            if (updatedAircraftModel == null)
            {
                _logger.LogWarning("API Response: Aircraft model with ID {AircraftModelId} not found for update", id);
                return NotFound();
            }
            
            _logger.LogInformation("API Response: Successfully updated aircraft model with ID {AircraftModelId}", id);
            
            return Ok(updatedAircraftModel);
        }

        /// <summary>
        /// Deletes an aircraft model
        /// </summary>
        /// <param name="id">The aircraft model ID</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the aircraft model was successfully deleted</response>
        /// <response code="404">If the aircraft model is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAircraftModel(int id)
        {
            _logger.LogInformation("API Request: Deleting aircraft model with ID {AircraftModelId}", id);
            
            var existingAircraftModel = _aircraftModelService.GetAircraftModelById(id);
            if (existingAircraftModel == null)
            {
                _logger.LogWarning("API Response: Aircraft model with ID {AircraftModelId} not found for deletion", id);
                return NotFound();
            }
            
            _aircraftModelService.DeleteAircraftModel(id);
            
            _logger.LogInformation("API Response: Successfully deleted aircraft model with ID {AircraftModelId}", id);
            
            return NoContent();
        }
    }
} 