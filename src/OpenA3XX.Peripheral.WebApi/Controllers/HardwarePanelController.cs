using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-panel")]
    public class HardwarePanelController : ControllerBase
    {
        private readonly IHardwarePanelService _hardwarePanelService;
        private readonly ILogger<HardwarePanelController> _logger;

        public HardwarePanelController(ILogger<HardwarePanelController> logger, IHttpContextAccessor accessor,
            IHardwarePanelService hardwarePanelTokensService)
        {
            _logger = logger;
            _hardwarePanelService = hardwarePanelTokensService;
        }

        [HttpPost("add")]
        public HardwarePanelDto AddHardwarePanel(HardwarePanelDto hardwarePanelDto)
        {
            return _hardwarePanelService.AddHardwarePanel(hardwarePanelDto);
        }

        [HttpGet("overview/all")]
        public IList<HardwarePanelOverviewDto> GetAllHardwarePanels()
        {
            var data = _hardwarePanelService.GetAllHardwarePanels();
            return data;
        }

        [HttpGet("details/{hardwarePanelId}")]
        public HardwarePanelDto GetHardwarePanelDetails(int hardwarePanelId)
        {
            var data = _hardwarePanelService.GetHardwarePanelDetails(hardwarePanelId);
            return data;
        }
    }
}