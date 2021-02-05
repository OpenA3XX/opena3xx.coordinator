using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Repositories;


namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<ConfigurationController> _logger;
        private readonly ISystemConfigurationRepository _systemConfigurationRepository;

        public ConfigurationController(ILogger<ConfigurationController> logger, IHttpContextAccessor accessor, ISystemConfigurationRepository systemConfigurationRepository)
        {
            _logger = logger;
            _accessor = accessor;
            _systemConfigurationRepository = systemConfigurationRepository;
        }
        
        /// <summary>
        /// Get all Configuration required in the hardware panels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SystemConfigurationResponseDto GetConfiguration()
        {
           var data =  _systemConfigurationRepository.GetAllConfiguration();
           var systemConfigurationResponse = new SystemConfigurationResponseDto
           {
               Configuration = new Dictionary<string, string>()
           };

           foreach (var config in data)
           {
               systemConfigurationResponse.Configuration.Add(config.Key, config.Value);
           }

           return systemConfigurationResponse;
        }
    }
}