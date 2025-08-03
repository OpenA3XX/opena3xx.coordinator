using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwareBoardService
    {
        IList<HardwareBoardDto> GetAllHardwareBoards();

        HardwareBoardDto SaveHardwareBoard(HardwareBoardDto hardwareBoardDto);

        HardwareBoardDetailsDto GetHardwareBoard(int id);

        HardwareBoardDetailsDto LinkExtenderBitToHardwareInputSelector(MapExtenderBitToHardwareInputSelectorDto linkExtenderBitToHardwareInputSelectorDto);
        
        HardwareBoardDetailsDto LinkExtenderBitToHardwareOutputSelector(MapExtenderBitToHardwareOutputSelectorDto linkExtenderBitToHardwareOutputSelectorDto);

        MapExtenderBitToHardwareInputSelectorDto GetHardwareBoardAssociationForHardwareInputSelector(
            int hardwareInputSelectorId);
        
        MapExtenderBitToHardwareOutputSelectorDto GetHardwareBoardAssociationForHardwareOutputSelector(
            int hardwareOutputSelectorId);
        
        /// <summary>
        /// Unmaps a hardware input selector from its current hardware board association
        /// </summary>
        /// <param name="hardwareInputSelectorId">The ID of the hardware input selector to unmap</param>
        void UnmapHardwareInputSelector(int hardwareInputSelectorId);
        
        /// <summary>
        /// Unmaps a hardware output selector from its current hardware board association
        /// </summary>
        /// <param name="hardwareOutputSelectorId">The ID of the hardware output selector to unmap</param>
        void UnmapHardwareOutputSelector(int hardwareOutputSelectorId);
        
        void Delete(int id);
        HardwareBoardDto Update(HardwareBoardDto hardwareBoardDto);
    }
}