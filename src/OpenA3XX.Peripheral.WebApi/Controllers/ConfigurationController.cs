using System.Collections.Generic;
using System.Linq;
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
            var data =  _systemConfigurationRepository.GetAllConfiguration()
                .ToDictionary(configuration => configuration.Key, configuration => configuration.Value);
           
            var systemConfigurationResponse = new SystemConfigurationResponseDto
            {
                Configuration = data
            };

            return systemConfigurationResponse;
        }
    }
}