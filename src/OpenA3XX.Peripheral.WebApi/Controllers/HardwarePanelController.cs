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
    [Route("hardware-panel")]
    public class HardwarePanelController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwarePanelTokensService _hardwarePanelTokensService;
        private readonly ILogger<HardwarePanelController> _logger;

        public HardwarePanelController(ILogger<HardwarePanelController> logger, IHttpContextAccessor accessor, IHardwarePanelTokensService hardwarePanelTokensService)
        {
            _logger = logger;
            _accessor = accessor;
            _hardwarePanelTokensService = hardwarePanelTokensService;
        }

        /// <summary>
        /// Get Hardware Panel by their respective Hardware Panel Id
        /// </summary>
        /// <param name="id">The Hardware Panel Id</param>
        /// <returns></returns>
        [HttpGet("details/{id}")]
        public HardwarePanelTokenDto GetByHardwarePanelId(int id)
        {
            var data = _hardwarePanelTokensService.GetByHardwarePanelId(id);

            return data;
        }
        
        /// <summary>
        /// Get Hardware Panel by Token.
        /// </summary>
        /// <param name="token">The Hardware Panel Token</param>
        /// <returns></returns>
        [HttpGet("details/token/{token}")]
        public HardwarePanelTokenDto GetByHardwarePanelToken(Guid token)
        {
            var data = _hardwarePanelTokensService.GetByHardwarePanelToken(token);

            return data;
        }
        
        /// <summary>
        /// Get all Hardware Panels.
        /// </summary>
        /// <returns></returns>
        [HttpGet("details")]
        public IList<HardwarePanelTokenDto> GetAllHardwarePanelTokens()
        {
            var data = _hardwarePanelTokensService.GetAllHardwarePanelTokens();

            return data;
        }
        
        
        /// <summary>
        /// Registering a new hardware panel.
        /// </summary>
        /// <param name="deviceRegistrationRequest">Information about the hardware panel to register</param>
        /// <returns></returns>
        [HttpPost]
        public HardwarePanelTokenDto RegisterHardwarePanel([FromBody] DeviceRegistrationRequestDto deviceRegistrationRequest)
        {
            return _hardwarePanelTokensService.RegisterHardwarePanel(deviceRegistrationRequest);
        }

        /// <summary>
        /// Endpoint which is used by hardware panels to notify their presence in the system.
        /// </summary>
        /// <param name="token">The Hardware Panel Token</param>
        /// <returns></returns>
        [HttpPost("keep-alive/{token}")]
        public void KeepAlive(Guid token)
        {
            _hardwarePanelTokensService.UpdateLastSeenForHardwarePane(token);
        }
    }
}