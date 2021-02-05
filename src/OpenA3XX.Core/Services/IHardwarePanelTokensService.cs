using System;
using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwarePanelTokensService
    {
        void UpdateLastSeenForHardwarePane(Guid token);

        HardwarePanelTokenDto GetByHardwarePanelId(int id);

        HardwarePanelTokenDto GetByHardwarePanelToken(Guid token);

        IList<HardwarePanelTokenDto> GetAllHardwarePanelTokens();

        HardwarePanelTokenDto RegisterHardwarePanel(DeviceRegistrationRequestDto deviceRegistrationRequest);

    }
}