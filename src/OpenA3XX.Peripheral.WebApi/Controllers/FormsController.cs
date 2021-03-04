using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Forms;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("forms")]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly ILogger<FormsController> _logger;

        public FormsController(ILogger<FormsController> logger, IFormService formService)
        {
            _logger = logger;
            _formService = formService;
        }

        /// <summary>
        ///     Ping endpoint used for network discovery functionality in the hardware panels
        /// </summary>
        /// <returns></returns>
        [HttpGet("settings")]
        public IList<FieldConfig> SettingsForm()
        {
            return _formService.GetSettingsFormFields();
        }
    }
}