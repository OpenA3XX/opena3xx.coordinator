using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-output-types")]
    public class HardwareOutputTypesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareOutputTypeService _hardwareOutputTypeService;
        private readonly ILogger<HardwareOutputTypesController> _logger;

        public HardwareOutputTypesController(ILogger<HardwareOutputTypesController> logger,
            IHttpContextAccessor accessor, IHardwareOutputTypeService hardwareOutputTypeService)
        {
            _logger = logger;
            _accessor = accessor;
            _hardwareOutputTypeService = hardwareOutputTypeService;
        }

        [HttpGet]
        public IList<HardwareOutputTypeDto> GetAll()
        {
            var data = _hardwareOutputTypeService.GetAll();

            return data;
        }

        [HttpGet("{hardwareOutputTypeId}")]
        public HardwareOutputTypeDto GetById(int hardwareOutputTypeId)
        {
            var data = _hardwareOutputTypeService.GetBy(hardwareOutputTypeId);

            return data;
        }

        [HttpPost]
        public IActionResult AddHardwareOutputType([FromBody] HardwareOutputTypeDto hardwareOutputTypeDto)
        {
            try
            {
                hardwareOutputTypeDto = _hardwareOutputTypeService.Add(hardwareOutputTypeDto);
                return Ok(hardwareOutputTypeDto);
            }
            catch (HardwareOutputTypeExistsException e)
            {
                return BadRequest(new ErrorDto
                {
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPatch]
        public HardwareOutputTypeDto UpdateHardwareOutputType([FromBody] HardwareOutputTypeDto hardwareOutputTypeDto)
        {
            hardwareOutputTypeDto = _hardwareOutputTypeService.Update(hardwareOutputTypeDto);
            return hardwareOutputTypeDto;
        }
    }
}