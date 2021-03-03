using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenA3XX.Core.Forms;
using OpenA3XX.Core.Repositories;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("forms")]
    public class FormsController : ControllerBase
    {
        private readonly ILogger<FormsController> _logger;
        private readonly IFormService _formService;

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