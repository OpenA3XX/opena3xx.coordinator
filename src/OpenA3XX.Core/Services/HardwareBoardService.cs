using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
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
            catch (Exception)
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
            catch (Exception)
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

        /// <summary>
        /// Deletes a hardware board and all its associated entities (cascade delete)
        /// </summary>
        /// <param name="id">The hardware board ID to delete</param>
        public void Delete(int id)
        {
            // Note: With the cascade delete configuration in CoreDataContext,
            // all related entities will be automatically deleted:
            // - IOExtenderBus records
            // - IOExtenderBit records (via IOExtenderBus)
            _hardwareBoardRepository.DeleteHardwareBoard(id);
        }

        /// <summary>
        /// Updates an existing hardware board
        /// </summary>
        /// <param name="hardwareBoardDto">The hardware board data to update</param>
        /// <returns>The updated hardware board</returns>
        public HardwareBoardDto Update(HardwareBoardDto hardwareBoardDto)
        {
            // Get the existing hardware board
            var existingHardwareBoard = _hardwareBoardRepository.GetByHardwareBoard(hardwareBoardDto.Id);
            if (existingHardwareBoard == null)
            {
                throw new EntityNotFoundException("HardwareBoard", hardwareBoardDto.Id);
            }

            // Update the basic properties
            existingHardwareBoard.Name = hardwareBoardDto.Name;

            // Handle the hardware bus extenders count change
            var currentBusCount = existingHardwareBoard.Buses?.Count ?? 0;
            var newBusCount = hardwareBoardDto.HardwareBusExtendersCount;

            if (newBusCount > currentBusCount)
            {
                // Add new buses
                for (var i = currentBusCount; i < newBusCount; i++)
                {
                    var extenderBus = new IOExtenderBus
                    {
                        HardwareBus = (HardwareBus)i,
                        Bits = new List<IOExtenderBit>()
                    };

                    // Add 16 bits for each bus (0-15)
                    for (var j = 0; j <= 15; j++)
                    {
                        extenderBus.Bits.Add(new IOExtenderBit()
                        {
                            HardwareBit = (HardwareBit)j
                        });
                    }

                    existingHardwareBoard.Buses.Add(extenderBus);
                }
            }
            else if (newBusCount < currentBusCount)
            {
                // Remove excess buses (from the end)
                var busesToRemove = currentBusCount - newBusCount;
                for (var i = 0; i < busesToRemove; i++)
                {
                    existingHardwareBoard.Buses.RemoveAt(existingHardwareBoard.Buses.Count - 1);
                }
            }

            // Save the updated hardware board
            var updatedHardwareBoard = _hardwareBoardRepository.UpdateHardwareBoard(existingHardwareBoard);
            return _mapper.Map<HardwareBoard, HardwareBoardDto>(updatedHardwareBoard);
        }
        
    }
}