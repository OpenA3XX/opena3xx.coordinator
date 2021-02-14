using System;
using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwarePanelService
    {
        void UpdateLastSeenForHardwarePane(Guid token);

        HardwarePanelTokenDto GetTokenDetailsByHardwarePanelId(int id);

        HardwarePanelTokenDto GetTokenDetailsByHardwarePanelToken(Guid token);

        IList<HardwarePanelTokenDto> GetAllHardwarePanelTokens();

        IList<HardwarePanelDto> GetAllHardwarePanels();

        HardwarePanelTokenDto RegisterHardwarePanel(DeviceRegistrationRequestDto deviceRegistrationRequest);
    }
}