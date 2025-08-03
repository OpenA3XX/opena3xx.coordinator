using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IHardwarePanelRepository
    {
        HardwarePanel AddHardwarePanel(HardwarePanel hardwarePanel);
        IList<HardwarePanel> GetAllHardwarePanels();

        HardwarePanel GetHardwarePanelDetails(int id);
        void DeleteHardwarePanel(int id);
        HardwarePanel UpdateHardwarePanel(HardwarePanel hardwarePanel);
    }
}