using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;
using OpenA3XX.Peripheral.WebApi.Models;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<ConfigurationController> _logger;
        private readonly ISystemConfigurationRepository _systemConfigurationRepository;

        public ConfigurationController(ILogger<ConfigurationController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CoreDataContext>();
            dbContextOptionsBuilder.UseSqlite(
                CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Core));

            _systemConfigurationRepository = new SystemConfigurationRepository(new CoreDataContext(dbContextOptionsBuilder.Options));
            
        }
        
        [HttpGet]
        public SystemConfigurationResponse GetConfiguration()
        {
           var data =  _systemConfigurationRepository.GetAllConfiguration();
           var systemConfigurationResponse = new SystemConfigurationResponse
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