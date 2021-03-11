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
        private readonly ISimulatorEventingService _simulatorEventingService;
        private readonly ISimulatorEventService _simulatorEventService;

        public SimulatorEventController(ILogger<SimulatorEventController> logger, IHttpContextAccessor accessor,
            ISimulatorEventService simulatorEventService,
            ISimulatorEventingService simulatorEventingService)
        {
            _logger = logger;
            _simulatorEventService = simulatorEventService;
            _simulatorEventingService = simulatorEventingService;
        }

        [HttpGet("all")]
        public IList<SimulatorEventDto> GetAll()
        {
            var data = _simulatorEventService.GetAllSimulatorEvents();
            return data;
        }

        [HttpGet("integration-types/all")]
        public IList<KeyValuePair<int, string>> GetAllIntegrationTypes()
        {
            var data = _simulatorEventService.GetAllIntegrationTypes();
            return data;
        }

        [HttpGet]
        public IList<SimulatorEventDto> GetByIntegrationType([FromQuery] int integrationTypeId)
        {
            var data = _simulatorEventService.GetByIntegrationType(integrationTypeId);
            return data;
        }

        [HttpPost("link/hardware-input-selector/{hardwareInputSelectorId}/{simulatorEventId}")]
        public SimulatorEventDto LinkSimulatorEventToInputSelector(int hardwareInputSelectorId, int simulatorEventId)
        {
            _simulatorEventService.SaveSimulatorEventLinking(simulatorEventId, 0, hardwareInputSelectorId);

            return new SimulatorEventDto();
        }

        [HttpPut("simulator-event/test/{simulatorEventId}")]
        public void TestSimulatorEvent(int simulatorEventId)
        {
            var simulatorEventDto = _simulatorEventService.GetSimulatorEventDetails(simulatorEventId);
            _simulatorEventingService.SendSimulatorTestEvent(simulatorEventDto);
        }
    }
}