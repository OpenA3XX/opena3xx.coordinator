using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services.Hardware;
using System;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using OpenA3XX.Core.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

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
            _logger.LogInformation("API Request: Getting all hardware input types from {ClientIP}", 
                _accessor.HttpContext?.Connection?.RemoteIpAddress);
                
            var data = _hardwareInputTypeService.GetAll();
            
            _logger.LogInformation("API Response: Returning {Count} hardware input types", data.Count);
            
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

        [HttpPost("debug/seed-test-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SeedTestData()
        {
            _logger.LogInformation("DEBUG: Seeding test hardware input types");
            
            try
            {
                // Create some test data
                var testInputTypes = new[]
                {
                    new HardwareInputTypeDto { Name = "Push Button" },
                    new HardwareInputTypeDto { Name = "Toggle Switch" },
                    new HardwareInputTypeDto { Name = "Rotary Encoder" }
                };
                
                foreach (var inputType in testInputTypes)
                {
                    try
                    {
                        var result = _hardwareInputTypeService.Add(inputType);
                        _logger.LogInformation("Created test input type: {Name} with ID: {Id}", result.Name, result.Id);
                    }
                                         catch (HardwareInputTypeExistsException)
                     {
                         _logger.LogInformation("Test input type already exists: {Name}", inputType.Name);
                     }
                }
                
                return Ok(new { message = "Test data seeded successfully", count = testInputTypes.Length });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to seed test data");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("debug/database-test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TestDatabaseConnection()
        {
            _logger.LogInformation("DEBUG: Testing direct database access");
            
            try
            {
                // Get the service and test direct access
                var data = _hardwareInputTypeService.GetAll();
                
                _logger.LogInformation("DEBUG: Service returned {Count} items", data.Count);
                
                return Ok(new 
                { 
                    message = "Database test completed", 
                    count = data.Count,
                    items = data.Select(x => new { x.Id, x.Name }).ToArray()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DEBUG: Database test failed");
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpGet("debug/connection-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetConnectionInfo()
        {
            _logger.LogInformation("DEBUG: Getting database connection information");
            
            try
            {
                // Get configuration using the registered options
                var openA3XXOptions = HttpContext.RequestServices.GetRequiredService<IOptions<OpenA3XXOptions>>();
                var configPath = openA3XXOptions?.Value?.Database?.Path;
                
                // Get actual connection string
                var connectionString = CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Core, configPath);
                
                // Extract database file path from connection string
                var dbFilePath = connectionString.Replace("Data Source=", "");
                
                // Check if file exists
                var fileExists = System.IO.File.Exists(dbFilePath);
                var fileSize = fileExists ? new FileInfo(dbFilePath).Length : 0;
                
                _logger.LogInformation("DEBUG: ConfigPath='{ConfigPath}', ConnectionString='{ConnectionString}', FileExists={FileExists}, FileSize={FileSize}", 
                    configPath, connectionString, fileExists, fileSize);
                
                return Ok(new 
                { 
                    configPath = configPath ?? "NULL",
                    connectionString = connectionString,
                    databaseFilePath = dbFilePath,
                    fileExists = fileExists,
                    fileSizeBytes = fileSize,
                    environment = Environment.GetEnvironmentVariable("OPENA3XX_DATABASE_PATH") ?? "NULL"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DEBUG: Failed to get connection info");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("debug/raw-sql-test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TestRawSql()
        {
            _logger.LogInformation("DEBUG: Testing raw SQL execution");
            
            try
            {
                // Get DbContext directly
                var dbContext = HttpContext.RequestServices.GetRequiredService<DbContext>();
                
                // Test raw SQL query
                var countResult = dbContext.Database.SqlQueryRaw<int>("SELECT COUNT(*) FROM HardwareInputType").ToArray();
                var count = countResult.FirstOrDefault();
                
                var nameResults = dbContext.Database.SqlQueryRaw<string>("SELECT Name FROM HardwareInputType LIMIT 3").ToArray();
                
                _logger.LogInformation("DEBUG: Raw SQL - Count: {Count}, Names: {Names}", 
                    count, string.Join(", ", nameResults));
                
                return Ok(new 
                { 
                    message = "Raw SQL test completed",
                    rawCount = count,
                    rawNames = nameResults,
                    dbContextType = dbContext.GetType().Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DEBUG: Raw SQL test failed");
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        /// <summary>
        /// Deletes a hardware input type from the system
        /// </summary>
        /// <param name="hardwareInputTypeId">The unique identifier of the hardware input type to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">If the hardware input type was successfully deleted</response>
        /// <response code="404">If the hardware input type is not found</response>
        /// <response code="400">If the hardware input type is being used by hardware inputs</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{hardwareInputTypeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult DeleteHardwareInputType(int hardwareInputTypeId)
        {
            _logger.LogInformation("API Request: Deleting hardware input type with ID {Id}", hardwareInputTypeId);

            try
            {
                _hardwareInputTypeService.Delete(hardwareInputTypeId);
                
                _logger.LogInformation("API Response: Successfully deleted hardware input type with ID {Id}", hardwareInputTypeId);
                
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                _logger.LogWarning("Hardware input type with ID {Id} not found for deletion", hardwareInputTypeId);
                throw;
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Cannot delete hardware input type with ID {Id}: {Message}", hardwareInputTypeId, ex.Message);
                return BadRequest(ErrorDto.Create(ex.Message, "HARDWARE_INPUT_TYPE_IN_USE"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete hardware input type with ID {Id}", hardwareInputTypeId);
                throw;
            }
        }
    }
}