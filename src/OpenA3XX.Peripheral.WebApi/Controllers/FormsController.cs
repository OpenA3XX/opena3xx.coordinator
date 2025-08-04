using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Forms;
using OpenA3XX.Core.Services.System;

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

        [HttpGet("settings")]
        public IList<FieldConfig> SettingsForm()
        {
            return _formService.GetSettingsFormFields();
        }

        [HttpGet("sim-link/input-selector/{hardwareInputSelectorId}")]
        public IList<FieldConfig> SimLinkInputSelectorForm(int hardwareInputSelectorId)
        {
            return _formService.GetSimLinkForHardwareInputSelectorIdForm(hardwareInputSelectorId);
        }

        [HttpGet("hardware-board-link/input-selector/{hardwareInputSelectorId}")]
        public IList<FieldConfig> HardwareBoardToHardwareInputSelectorForm(int hardwareInputSelectorId)
        {
            return _formService.GetHardwareInputSelectorToBoardForm(hardwareInputSelectorId);
        }
    }
}