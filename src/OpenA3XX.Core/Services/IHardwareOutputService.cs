using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwareOutputService
    {
        IList<HardwareOutputDto> GetAll();

        IList<HardwareOutputDto> GetByPanelId(int panelId);

        HardwareOutputDto GetBy(int id);

        HardwareOutputDto Add(HardwareOutputDto hardwareOutputDto);

        HardwareOutputDto Update(HardwareOutputDto hardwareOutputDto);

        void Delete(int id);
    }
} 