using System.Collections.Generic;
using System.Linq;
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
        private readonly IHardwareInputSelectorService _hardwareInputSelectorService;

        public HardwareBoardsController(ILogger<HardwareBoardsController> logger, IHardwareBoardService hardwareBoardService, IHardwareInputSelectorService hardwareInputSelectorService)
        {
            _logger = logger;
            _hardwareBoardService = hardwareBoardService;
            _hardwareInputSelectorService = hardwareInputSelectorService;
        }

        [HttpGet("all")]
        public IList<HardwareBoardDto> GetAllHardwareBoards()
        {
            var hardwareBoardDtos = _hardwareBoardService.GetAllHardwareBoards();
            return hardwareBoardDtos;
        }

        [HttpGet("{hardwareBoardId}")]
        public HardwareBoardDetailsDto GetHardwareBoardDetails(int hardwareBoardId)
        {
            var hardwareBoardDetailsDto = _hardwareBoardService.GetHardwareBoard(hardwareBoardId);
            return hardwareBoardDetailsDto;
        }
        
        [HttpPost("add")]
        public HardwareBoardDto RegisterHardwareBoard([FromBody] HardwareBoardDto hardwareBoardDto)
        {
            var data = _hardwareBoardService.SaveHardwareBoard(hardwareBoardDto);
            return data;
        }

        [HttpPost("link/hardware-input-selector")]
        public HardwareBoardDetailsDto LinkExtenderBitToHardwareInputSelector([FromBody] MapExtenderBitToHardwareInputSelectorDto linkExtenderBitToHardwareInputSelectorDto)
        {
            var hardwareBoardDetailsDto = _hardwareBoardService.LinkExtenderBitToHardwareInputSelector(linkExtenderBitToHardwareInputSelectorDto);
            return hardwareBoardDetailsDto;
        }
        
        [HttpPost("link/hardware-output-selector")]
        public HardwareBoardDetailsDto LinkExtenderBitToHardwareOutputSelector([FromBody] MapExtenderBitToHardwareOutputSelectorDto linkExtenderBitToHardwareOutputSelectorDto)
        {
            var hardwareBoardDetailsDto = _hardwareBoardService.LinkExtenderBitToHardwareOutputSelector(linkExtenderBitToHardwareOutputSelectorDto);
            return hardwareBoardDetailsDto;
        }
        

        [HttpGet("hardware-input-selector/{hardwareInputSelectorId}")]
        public MapExtenderBitToHardwareInputSelectorDto GetHardwareBoardAssociationForHardwareInputSelector(int hardwareInputSelectorId)
        {
            var linkExtenderBitToHardwareInputSelectorDto = _hardwareBoardService.GetHardwareBoardAssociationForHardwareInputSelector(hardwareInputSelectorId);
            return linkExtenderBitToHardwareInputSelectorDto;
        }
        
        [HttpGet("hardware-output-selector/{hardwareOutputSelectorId}")]
        public MapExtenderBitToHardwareOutputSelectorDto GetHardwareBoardAssociationForHardwareOutputSelector(int hardwareOutputSelectorId)
        {
            var linkExtenderBitToHardwareOutputSelectorDto = _hardwareBoardService.GetHardwareBoardAssociationForHardwareOutputSelector(hardwareOutputSelectorId);
            return linkExtenderBitToHardwareOutputSelectorDto;
        }
    }

}