using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("core")]
    public class CoreController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<HardwarePanelController> _logger;

        public CoreController(ILogger<HardwarePanelController> logger, IHttpContextAccessor accessor,
            IHardwarePanelTokensRepository hardwarePanelTokensRepository)
        {
            _logger = logger;
            _accessor = accessor;
        }

        /// <summary>
        ///     Ping endpoint used for network discovery functionality in the hardware panels
        /// </summary>
        /// <returns></returns>
        [HttpGet("heartbeat/ping")]
        public string Ping()
        {
            return "Pong from OpenA3XX";
        }
    }
}