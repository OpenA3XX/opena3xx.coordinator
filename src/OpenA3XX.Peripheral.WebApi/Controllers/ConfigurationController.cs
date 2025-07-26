using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<ConfigurationController> _logger;
        private readonly RabbitMQOptions _rabbitMqOptions;
        private readonly SeqOptions _seqOptions;

        public ConfigurationController(
            ILogger<ConfigurationController> logger, 
            IHttpContextAccessor accessor,
            IOptions<RabbitMQOptions> rabbitMqOptions,
            IOptions<SeqOptions> seqOptions)
        {
            _logger = logger;
            _accessor = accessor;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _seqOptions = seqOptions.Value;
        }

        /// <summary>
        ///     Get all Configuration required in the hardware panels (read-only from appsettings)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Dictionary<string, string> GetConfiguration()
        {
            var data = new Dictionary<string, string>
            {
                // RabbitMQ Configuration
                ["opena3xx-amqp-host"] = _rabbitMqOptions.HostName,
                ["opena3xx-amqp-port"] = _rabbitMqOptions.Port.ToString(),
                ["opena3xx-amqp-username"] = _rabbitMqOptions.Username,
                ["opena3xx-amqp-password"] = _rabbitMqOptions.Password,
                ["opena3xx-amqp-vhost"] = _rabbitMqOptions.VirtualHost,
                ["opena3xx-amqp-keepalive-exchange-bindings-configuration"] = _rabbitMqOptions.ExchangeBindings.KeepAlive,
                ["opena3xx-amqp-hardware-input-selectors-exchange-bindings-configuration"] = _rabbitMqOptions.ExchangeBindings.HardwareInputSelectors,
                
                // SEQ Configuration
                ["opena3xx-logging-seq-host"] = _seqOptions.Host,
                ["opena3xx-logging-seq-port"] = _seqOptions.Port.ToString()
            };

            return data;
        }

        /// <summary>
        /// Update configuration endpoint - now disabled since configuration is in appsettings
        /// </summary>
        [HttpPost]
        public IActionResult UpdateAllConfiguration(Dictionary<string, string> configurationList)
        {
            return BadRequest(new { message = "Configuration updates are disabled. All configuration is now managed through appsettings.json files." });
        }
    }
}