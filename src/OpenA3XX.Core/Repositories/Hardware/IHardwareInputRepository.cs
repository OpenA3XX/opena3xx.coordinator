using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Hardware
{
    public interface IHardwareInputRepository
    {
        IList<HardwareInput> GetAllHardwareInputs();

        IList<HardwareInput> GetHardwareInputsByPanelId(int panelId);

        HardwareInput GetHardwareInputBy(int id);

        HardwareInput AddHardwareInput(HardwareInput hardwareInput);

        HardwareInput UpdateHardwareInput(HardwareInput hardwareInput);

        void DeleteHardwareInput(int id);
    }
} 