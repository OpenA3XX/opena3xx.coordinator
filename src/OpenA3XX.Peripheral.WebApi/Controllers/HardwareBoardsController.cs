using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Services;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("hardware-boards")]
    public class HardwareBoardsController
    {
        private readonly ILogger<HardwareBoardsController> _logger;
        private readonly IHardwareBoardService _hardwareBoardService;

        public HardwareBoardsController(ILogger<HardwareBoardsController> logger, IHardwareBoardService hardwareBoardService)
        {
            _logger = logger;
            _hardwareBoardService = hardwareBoardService;
        }

        [HttpGet("all")]
        public IList<HardwareBoardDto> GetAllHardwareBoards()
        {
            var data = _hardwareBoardService.GetAllHardwareBoards();
            return data;
        }
    }
}