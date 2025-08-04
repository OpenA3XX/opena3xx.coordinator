using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services.Hardware
{
    public interface IHardwareInputService
    {
        IList<HardwareInputDto> GetAll();

        IList<HardwareInputDto> GetByPanelId(int panelId);

        HardwareInputDto GetBy(int id);

        HardwareInputDto Add(HardwareInputDto hardwareInputDto);

        HardwareInputDto Update(HardwareInputDto hardwareInputDto);

        void Delete(int id);
    }
} 