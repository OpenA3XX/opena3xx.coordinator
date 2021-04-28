using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-output-selectors")]
    public class HardwareOutputSelectorsController : ControllerBase
    {
        private readonly IHardwareOutputSelectorService _hardwareOutputSelectorService;
        private readonly ILogger<HardwareOutputSelectorsController> _logger;

        public HardwareOutputSelectorsController(ILogger<HardwareOutputSelectorsController> logger,
            IHardwareOutputSelectorService hardwareOutputSelectorService
        )
        {
            _logger = logger;
            _hardwareOutputSelectorService = hardwareOutputSelectorService;
        }

        [HttpGet("{hardwareOutputSelectorId}")]
        public HardwareOutputSelectorDto GetHardwareOutputSelectorDetails(int hardwareOutputSelectorId)
        {
            var data = _hardwareOutputSelectorService.GetHardwareOutputSelectorDetails(hardwareOutputSelectorId);
            return data;
        }
    }
}