using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
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
            var hardwareBoardDtos = _hardwareBoardService.GetAllHardwareBoards();
            return hardwareBoardDtos;
        }

        [HttpGet("{id}")]
        public HardwareBoardDetailsDto GetHardwareBoardDetails(int id)
        {
            var hardwareBoardDetailsDto = _hardwareBoardService.GetHardwareBoard(id);
            return hardwareBoardDetailsDto;
        }
        
        [HttpPost("add")]
        public HardwareBoardDto RegisterHardwareBoard([FromBody] HardwareBoardDto hardwareBoardDto)
        {
            var data = _hardwareBoardService.SaveHardwareBoard(hardwareBoardDto);
            return data;
        }

        [HttpPost("link/hardware-input-selector")]
        public HardwareBoardDetailsDto LinkExtenderBitToHardwareInputSelector([FromBody] LinkExtenderBitToHardwareInputSelectorDto linkExtenderBitToHardwareInputSelectorDto)
        {
            var hardwareBoardDetailsDto = _hardwareBoardService.LinkExtenderBitToHardwareInputSelector(linkExtenderBitToHardwareInputSelectorDto);
            return hardwareBoardDetailsDto;
        }
        
    }

}