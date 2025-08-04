using System;
using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Hardware
{
    public interface IHardwarePanelTokensRepository
    {
        HardwarePanelToken GetByHardwarePanelToken(Guid hardwarePanelToken);
        HardwarePanelToken GetByHardwarePanelId(int id);
        IList<HardwarePanelToken> GetAllHardwarePanelTokens();
        HardwarePanelToken SaveHardwarePanelToken(HardwarePanelToken hardwarePanelToken);

        HardwarePanelToken UpdateLastSeenForHardwarePanel(Guid hardwarePanelToken);
    }
}