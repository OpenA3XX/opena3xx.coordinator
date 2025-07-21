using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Repositories;
using OpenA3XX.Peripheral.WebApi.Hubs;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    /// <summary>
    /// Core controller providing fundamental system operations and health checks.
    /// Includes network discovery functionality for hardware panel integration.
    /// </summary>
    [ApiController]
    [Route("core")]
    [Produces("application/json")]
    public class CoreController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<CoreController> _logger;

        /// <summary>
        /// Initializes a new instance of the CoreController
        /// </summary>
        /// <param name="logger">Logger instance for this controller</param>
        /// <param name="accessor">HTTP context accessor for request information</param>
        /// <param name="hardwarePanelTokensRepository">Repository for hardware panel token operations</param>
        public CoreController(ILogger<CoreController> logger, IHttpContextAccessor accessor,
            IHardwarePanelTokensRepository hardwarePanelTokensRepository)
        {
            _logger = logger;
            _accessor = accessor;
        }

        /// <summary>
        /// Health check endpoint used for network discovery functionality in the hardware panels.
        /// This endpoint helps hardware panels discover and verify connectivity to the coordinator.
        /// </summary>
        /// <returns>A simple "pong" response confirming the service is operational</returns>
        /// <response code="200">Returns confirmation that the service is running and accessible</response>
        /// <response code="500">If an internal server error occurs</response>
        /// <remarks>
        /// This endpoint is commonly used by:
        /// - Hardware panels during network discovery
        /// - Monitoring systems for health checks
        /// - Load balancers for service availability verification
        /// </remarks>
        [HttpGet("heartbeat/ping")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public string Ping()
        {
            _logger.LogDebug("Ping request received from {RemoteIpAddress}", 
                _accessor.HttpContext?.Connection.RemoteIpAddress);
            
            return "Pong from OpenA3XX";
        }
    }
}