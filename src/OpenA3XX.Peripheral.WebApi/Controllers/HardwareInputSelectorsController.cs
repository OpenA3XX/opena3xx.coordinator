using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-input-selectors")]
    public class HardwareInputSelectorsController : ControllerBase
    {
        private readonly IHardwareInputSelectorService _hardwareInputSelectorService;
        private readonly ILogger<HardwareInputSelectorsController> _logger;

        public HardwareInputSelectorsController(ILogger<HardwareInputSelectorsController> logger,
            IHardwareInputSelectorService hardwareInputSelectorService
        )
        {
            _logger = logger;
            _hardwareInputSelectorService = hardwareInputSelectorService;
        }

        [HttpGet("{hardwareInputSelectorId}")]
        public HardwareInputSelectorDto GetHardwareInputSelectorDetails(int hardwareInputSelectorId)
        {
            var data = _hardwareInputSelectorService.GetHardwareInputSelectorDetails(hardwareInputSelectorId);
            return data;
        }
    }
}