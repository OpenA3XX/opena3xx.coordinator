using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-input-types")]
    public class HardwareInputTypesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareInputTypeService _hardwareInputTypeService;
        private readonly ILogger<HardwareInputTypesController> _logger;

        public HardwareInputTypesController(ILogger<HardwareInputTypesController> logger, IHttpContextAccessor accessor, IHardwareInputTypeService hardwareInputTypeService)
        {
            _logger = logger;
            _accessor = accessor;
            _hardwareInputTypeService = hardwareInputTypeService;
        }
        
        [HttpGet]
        public IList<HardwareInputTypeDto> GetAll()
        {
            var data = _hardwareInputTypeService.GetAll();

            return data;
        }
    }
}