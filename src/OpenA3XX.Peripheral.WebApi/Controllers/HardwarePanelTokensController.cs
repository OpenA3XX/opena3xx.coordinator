using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-panel-tokens")]
    public class HardwarePanelTokensController : ControllerBase
    {
        private readonly IHardwarePanelService _hardwarePanelService;
        private readonly ILogger<HardwarePanelController> _logger;

        public HardwarePanelTokensController(ILogger<HardwarePanelController> logger, IHttpContextAccessor accessor,
            IHardwarePanelService hardwarePanelTokensService)
        {
            _logger = logger;
            _hardwarePanelService = hardwarePanelTokensService;
        }


        [HttpGet("details/panel-id/{hardwarePanelId}")]
        public HardwarePanelTokenDto GetTokenDetailsByHardwarePanelId(int hardwarePanelId)
        {
            var data = _hardwarePanelService.GetTokenDetailsByHardwarePanelId(hardwarePanelId);

            return data;
        }

        [HttpGet("details/token/{hardwarePanelToken}")]
        public HardwarePanelTokenDto GetByHardwarePanelToken(Guid hardwarePanelToken)
        {
            var data = _hardwarePanelService.GetTokenDetailsByHardwarePanelToken(hardwarePanelToken);

            return data;
        }


        [HttpGet("details/all")]
        public IList<HardwarePanelTokenDto> GetAllHardwarePanelDetails()
        {
            var data = _hardwarePanelService.GetAllHardwarePanelTokens();

            return data;
        }


        [HttpPost("register")]
        public HardwarePanelTokenDto RegisterHardwarePanel(
            [FromBody] DeviceRegistrationRequestDto deviceRegistrationRequest)
        {
            return _hardwarePanelService.RegisterHardwarePanel(deviceRegistrationRequest);
        }

        [HttpPost("keep-alive/{token}")]
        public void KeepAlive(Guid token)
        {
            _hardwarePanelService.UpdateLastSeenForHardwarePane(token);
        }
    }
}