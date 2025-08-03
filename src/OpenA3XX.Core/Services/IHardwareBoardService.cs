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
        
        void Delete(int id);
        HardwareBoardDto Update(HardwareBoardDto hardwareBoardDto);
    }
}