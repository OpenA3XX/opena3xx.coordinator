using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services.System;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Controller for checking system dependency status
    /// </summary>
    [ApiController]
    [Route("dependency-status")]
    [Produces("application/json")]
    public class DependencyStatusController : ControllerBase
    {
        private readonly IDependencyStatusService _dependencyStatusService;
        private readonly ILogger<DependencyStatusController> _logger;

        public DependencyStatusController(
            IDependencyStatusService dependencyStatusService, 
            ILogger<DependencyStatusController> logger)
        {
            _dependencyStatusService = dependencyStatusService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the status of all system dependencies
        /// </summary>
        /// <returns>Dependency status information</returns>
        /// <response code="200">Returns dependency status information</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(DependencyStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DependencyStatusDto>> GetDependencyStatus()
        {
            _logger.LogInformation("Dependency status check requested");

            try
            {
                var status = await _dependencyStatusService.GetDependencyStatusAsync();
                
                _logger.LogInformation("Dependency status check completed successfully. Overall healthy: {IsHealthy}", status.IsHealthy);
                
                return Ok(status);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking dependency status");
                return StatusCode(StatusCodes.Status500InternalServerError, new { 
                    error = "Failed to check dependency status", 
                    message = ex.Message 
                });
            }
        }

        /// <summary>
        /// Gets the status of Microsoft Flight Simulator
        /// </summary>
        /// <returns>MSFS dependency status</returns>
        /// <response code="200">Returns MSFS status information</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("msfs")]
        [ProducesResponseType(typeof(DependencyDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DependencyDetailDto>> GetMsfsStatus()
        {
            _logger.LogInformation("MSFS status check requested");

            try
            {
                var status = await _dependencyStatusService.CheckMsfsStatusAsync();
                
                _logger.LogInformation("MSFS status check completed. Running: {IsRunning}", status.IsRunning);
                
                return Ok(status);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking MSFS status");
                return StatusCode(StatusCodes.Status500InternalServerError, new { 
                    error = "Failed to check MSFS status", 
                    message = ex.Message 
                });
            }
        }

        /// <summary>
        /// Gets the status of RabbitMQ
        /// </summary>
        /// <returns>RabbitMQ dependency status</returns>
        /// <response code="200">Returns RabbitMQ status information</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("rabbitmq")]
        [ProducesResponseType(typeof(DependencyDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DependencyDetailDto>> GetRabbitMqStatus()
        {
            _logger.LogInformation("RabbitMQ status check requested");

            try
            {
                var status = await _dependencyStatusService.CheckRabbitMqStatusAsync();
                
                _logger.LogInformation("RabbitMQ status check completed. Running: {IsRunning}", status.IsRunning);
                
                return Ok(status);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking RabbitMQ status");
                return StatusCode(StatusCodes.Status500InternalServerError, new { 
                    error = "Failed to check RabbitMQ status", 
                    message = ex.Message 
                });
            }
        }

        /// <summary>
        /// Gets the status of SEQ logging service
        /// </summary>
        /// <returns>SEQ dependency status</returns>
        /// <response code="200">Returns SEQ status information</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("seq")]
        [ProducesResponseType(typeof(DependencyDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DependencyDetailDto>> GetSeqStatus()
        {
            _logger.LogInformation("SEQ status check requested");

            try
            {
                var status = await _dependencyStatusService.CheckSeqStatusAsync();
                
                _logger.LogInformation("SEQ status check completed. Running: {IsRunning}", status.IsRunning);
                
                return Ok(status);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking SEQ status");
                return StatusCode(StatusCodes.Status500InternalServerError, new { 
                    error = "Failed to check SEQ status", 
                    message = ex.Message 
                });
            }
        }

        /// <summary>
        /// Simple health check endpoint
        /// </summary>
        /// <returns>Simple health status</returns>
        /// <response code="200">System is healthy</response>
        /// <response code="503">One or more dependencies are not running</response>
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult> GetHealth()
        {
            try
            {
                var status = await _dependencyStatusService.GetDependencyStatusAsync();
                
                if (status.IsHealthy)
                {
                    return Ok(new { status = "healthy", timestamp = status.CheckedAt });
                }
                else
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, 
                        new { status = "unhealthy", timestamp = status.CheckedAt });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred during health check");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, 
                    new { status = "unhealthy", error = ex.Message });
            }
        }
    }
} 