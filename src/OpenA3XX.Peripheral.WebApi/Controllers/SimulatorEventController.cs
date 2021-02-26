using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("simulator-event")]
    public class SimulatorEventController : ControllerBase
    {
        private readonly ISimulatorEventRepository _simulatorEventRepository;
        private readonly ISimulatorEventLinkRepository _simulatorEventLinkRepository;
        private readonly ILogger<SimulatorEventController> _logger;

        public SimulatorEventController(ILogger<SimulatorEventController> logger, IHttpContextAccessor accessor, ISimulatorEventRepository simulatorEventRepository, ISimulatorEventLinkRepository simulatorEventLinkRepository)
        {
            _logger = logger;
            _simulatorEventRepository = simulatorEventRepository;
            _simulatorEventLinkRepository = simulatorEventLinkRepository;
        }

        [HttpPost]
        public SimulatorEvent AddSimulatorEvent(SimulatorEvent simulatorEvent)
        {
            return new SimulatorEvent();
        }

        [HttpGet("links")]
        public IList<SimulatorEventLink> GetAllSimulatorEventLinks()
        {
            var data = _simulatorEventLinkRepository.GetAllSimulatorEventLinks();
            return data;
        }
    }
}