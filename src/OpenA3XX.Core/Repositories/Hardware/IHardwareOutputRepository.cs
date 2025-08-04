using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Hardware
{
    public interface IHardwareOutputRepository
    {
        IList<HardwareOutput> GetAllHardwareOutputs();

        IList<HardwareOutput> GetHardwareOutputsByPanelId(int panelId);

        HardwareOutput GetHardwareOutputBy(int id);

        HardwareOutput AddHardwareOutput(HardwareOutput hardwareOutput);

        HardwareOutput UpdateHardwareOutput(HardwareOutput hardwareOutput);

        void DeleteHardwareOutput(int id);
    }
} 