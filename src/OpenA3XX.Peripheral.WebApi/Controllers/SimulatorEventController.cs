using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("simulator-event")]
    public class SimulatorEventController : ControllerBase
    {
        private readonly ILogger<SimulatorEventController> _logger;
        private readonly ISimulatorEventService _simulatorEventService;

        public SimulatorEventController(ILogger<SimulatorEventController> logger, IHttpContextAccessor accessor,
            ISimulatorEventService simulatorEventService)
        {
            _logger = logger;
            _simulatorEventService = simulatorEventService;
        }

        [HttpGet("all")]
        public IList<SimulatorEventDto> GetAll()
        {
            var data = _simulatorEventService.GetAllSimulatorEvents();
            return data;
        }
    }
}