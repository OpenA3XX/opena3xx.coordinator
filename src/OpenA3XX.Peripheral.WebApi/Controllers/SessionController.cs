using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;
using OpenA3XX.Peripheral.WebApi.Models;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("session")]
    public class SessionController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<SessionController> _logger;
        private readonly IHardwarePanelTokensRepository _hardwarePanelTokensRepository;

        public SessionController(ILogger<SessionController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CoreDataContext>();
            dbContextOptionsBuilder.UseSqlite(
                CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Core));

            _hardwarePanelTokensRepository = new HardwarePanelTokensRepository(new CoreDataContext(dbContextOptionsBuilder.Options));
            
        }

        
        [HttpGet("panel/{id}")]
        public HardwarePanelToken GetByHardwarePanelId(int id)
        {
            var data = _hardwarePanelTokensRepository.GetByHardwarePanelId(id);

            return data;
        }
        
        [HttpGet("token/{token}")]
        public HardwarePanelToken GetByHardwarePanelToken(Guid token)
        {
            var data = _hardwarePanelTokensRepository.GetByHardwarePanelToken(token);

            return data;
        }
        
        [HttpPost("heartbeat/{token}")]
        public HardwarePanelToken Heartbeat(Guid token)
        {
            var data = _hardwarePanelTokensRepository.UpdateLastSeenForHardwarePanel(token);

            return data;
        }
        

        [HttpPost]
        public HardwarePanelToken Post([FromBody] DeviceRegistrationRequest deviceRegistrationRequest)
        {
            var hardwarePanelToken = new HardwarePanelToken
            {
                DeviceToken = Guid.NewGuid().ToString(),
                HardwarePanelId = deviceRegistrationRequest.HardwarePanelId,
                CreatedDateTime = DateTime.Now,
                DeviceIpAddress = _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
            };
            return _hardwarePanelTokensRepository.SaveHardwarePanelToken(hardwarePanelToken);

        }
    }
}