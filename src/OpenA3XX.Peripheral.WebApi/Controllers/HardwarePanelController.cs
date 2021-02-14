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

        [HttpGet("details/all")]
        public IList<HardwarePanelDto> GetAllHardwarePanels()
        {
            var data = _hardwarePanelService.GetAllHardwarePanels();

            return data;
        }


    }
}