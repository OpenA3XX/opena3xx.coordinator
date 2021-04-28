using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class HardwareBoardService : IHardwareBoardService
    {
        private readonly IHardwareBoardRepository _hardwareBoardRepository;
        private readonly IMapper _mapper;

        public HardwareBoardService(IHardwareBoardRepository hardwareBoardRepository, IMapper mapper)
        {
            _hardwareBoardRepository = hardwareBoardRepository;
            _mapper = mapper;
        }

        public IList<HardwareBoardDto> GetAllHardwareBoards()
        {
            var hardwareBoards = _hardwareBoardRepository.GetAllHardwareBoards();
            var hardwareBoardDtoList = _mapper.Map<IList<HardwareBoard>, IList<HardwareBoardDto>>(hardwareBoards);
            return hardwareBoardDtoList;
        }

        public HardwareBoardDto SaveHardwareBoard(HardwareBoardDto hardwareBoardDto)
        {
            var hardwareBoard = new HardwareBoard
            {
                Name = hardwareBoardDto.Name,
                Buses = new List<IOExtenderBus>()
            };
            for (var i = 0; i < hardwareBoardDto.HardwareBusExtendersCount; i++)
            {
                var extenderBus = new IOExtenderBus
                {
                    HardwareBus = (HardwareBus) i, Bits = new List<IOExtenderBit>(15)
                };

                extenderBus.Bits = new List<IOExtenderBit>();
                for (var j = 0; j <= 15; j++)
                {
                    extenderBus.Bits.Add(new IOExtenderBit()
                    {
                        HardwareBit = (HardwareBit) j
                    });
                }

                hardwareBoard.Buses.Add(extenderBus);
            }

            var savedHardwareBoard = _hardwareBoardRepository.SaveHardwareBoard(hardwareBoard);
            return _mapper.Map<HardwareBoard, HardwareBoardDto>(savedHardwareBoard);
        }

        public HardwareBoardDetailsDto GetHardwareBoard(int id)
        {
            var hardwareBoard = _hardwareBoardRepository.GetByHardwareBoard(id);
            var hardwareBoardDetailsDto = _mapper.Map<HardwareBoard, HardwareBoardDetailsDto>(hardwareBoard);
            return hardwareBoardDetailsDto;
        }

        public HardwareBoardDetailsDto LinkExtenderBitToHardwareInputSelector(
            MapExtenderBitToHardwareInputSelectorDto linkExtenderBitToHardwareInputSelectorDto)
        {
            try
            {
                //--- Remove old map
                var currentLink = GetHardwareBoardAssociationForHardwareInputSelector(linkExtenderBitToHardwareInputSelectorDto
                    .HardwareInputSelectorId);

                var currentBoard = _hardwareBoardRepository.GetByHardwareBoard(currentLink.HardwareBoardId);
                currentBoard.Buses
                    .First(c => c.Id == currentLink.HardwareExtenderBusId).Bits.First(x => x.Id == currentLink.HardwareExtenderBusBitId)
                    .HardwareInputSelector = null;
                _hardwareBoardRepository.UpdateHardwareBoard(currentBoard);
            }
            catch (Exception ex)
            {
                // ignored
            }

            //--- Update to new map
            
            var hardwareBoard =
                _hardwareBoardRepository.GetByHardwareBoard(linkExtenderBitToHardwareInputSelectorDto.HardwareBoardId);
            hardwareBoard.Buses
                .First(c => c.Id == linkExtenderBitToHardwareInputSelectorDto.HardwareExtenderBusId).Bits
                .First(c => c.Id == linkExtenderBitToHardwareInputSelectorDto.HardwareExtenderBusBitId)
                .HardwareInputSelectorId = linkExtenderBitToHardwareInputSelectorDto.HardwareInputSelectorId;

            _hardwareBoardRepository.UpdateHardwareBoard(hardwareBoard);
            return GetHardwareBoard(hardwareBoard.Id);
        }
        
        public HardwareBoardDetailsDto LinkExtenderBitToHardwareOutputSelector(
            MapExtenderBitToHardwareOutputSelectorDto linkExtenderBitToHardwareOutputSelectorDto)
        {
            try
            {
                //--- Remove old map
                var currentLink = GetHardwareBoardAssociationForHardwareOutputSelector(linkExtenderBitToHardwareOutputSelectorDto
                    .HardwareOutputSelectorId);

                var currentBoard = _hardwareBoardRepository.GetByHardwareBoard(currentLink.HardwareBoardId);
                currentBoard.Buses
                    .First(c => c.Id == currentLink.HardwareExtenderBusId).Bits.First(x => x.Id == currentLink.HardwareExtenderBusBitId)
                    .HardwareOutputSelector = null;
                _hardwareBoardRepository.UpdateHardwareBoard(currentBoard);
            }
            catch (Exception ex)
            {
                // ignored
            }

            //--- Update to new map
            
            var hardwareBoard =
                _hardwareBoardRepository.GetByHardwareBoard(linkExtenderBitToHardwareOutputSelectorDto.HardwareBoardId);
            hardwareBoard.Buses
                .First(c => c.Id == linkExtenderBitToHardwareOutputSelectorDto.HardwareExtenderBusId).Bits
                .First(c => c.Id == linkExtenderBitToHardwareOutputSelectorDto.HardwareExtenderBusBitId)
                .HardwareOutputSelectorId = linkExtenderBitToHardwareOutputSelectorDto.HardwareOutputSelectorId;

            _hardwareBoardRepository.UpdateHardwareBoard(hardwareBoard);
            return GetHardwareBoard(hardwareBoard.Id);
        }
        

        public MapExtenderBitToHardwareInputSelectorDto GetHardwareBoardAssociationForHardwareInputSelector(
            int hardwareInputSelectorId)
        {
            var linkExtenderBitToHardwareInputSelectorDto = new MapExtenderBitToHardwareInputSelectorDto();


            foreach (var board in _hardwareBoardRepository.GetAllHardwareBoards())
            {
                foreach (var bus in board.Buses)
                {
                    foreach (var bit in bus.Bits)
                    {
                        if (bit.HardwareInputSelector == null) continue;
                        if (bit.HardwareInputSelector.Id != hardwareInputSelectorId) continue;
                        linkExtenderBitToHardwareInputSelectorDto.HardwareBoardId = board.Id;
                        linkExtenderBitToHardwareInputSelectorDto.HardwareExtenderBusId = bus.Id;
                        linkExtenderBitToHardwareInputSelectorDto.HardwareExtenderBusBitId = bit.Id;
                        linkExtenderBitToHardwareInputSelectorDto.HardwareInputSelectorId = hardwareInputSelectorId;
                        return linkExtenderBitToHardwareInputSelectorDto;

                    }
                }
            }

            return linkExtenderBitToHardwareInputSelectorDto;
        }
        
        public MapExtenderBitToHardwareOutputSelectorDto GetHardwareBoardAssociationForHardwareOutputSelector(
            int hardwareOutputSelectorId)
        {
            var linkExtenderBitToHardwareOutputSelectorDto = new MapExtenderBitToHardwareOutputSelectorDto();


            foreach (var board in _hardwareBoardRepository.GetAllHardwareBoards())
            {
                foreach (var bus in board.Buses)
                {
                    foreach (var bit in bus.Bits)
                    {
                        if (bit.HardwareOutputSelector == null) continue;
                        if (bit.HardwareOutputSelector.Id != hardwareOutputSelectorId) continue;
                        linkExtenderBitToHardwareOutputSelectorDto.HardwareBoardId = board.Id;
                        linkExtenderBitToHardwareOutputSelectorDto.HardwareExtenderBusId = bus.Id;
                        linkExtenderBitToHardwareOutputSelectorDto.HardwareExtenderBusBitId = bit.Id;
                        linkExtenderBitToHardwareOutputSelectorDto.HardwareOutputSelectorId = hardwareOutputSelectorId;
                        return linkExtenderBitToHardwareOutputSelectorDto;

                    }
                }
            }

            return linkExtenderBitToHardwareOutputSelectorDto;
        }
        
    }
}