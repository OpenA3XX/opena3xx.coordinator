using System;
using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwarePanelService
    {
        void UpdateLastSeenForHardwarePane(Guid token);

        //Tokens
        HardwarePanelTokenDto GetTokenDetailsByHardwarePanelId(int id);

        HardwarePanelTokenDto GetTokenDetailsByHardwarePanelToken(Guid token);

        IList<HardwarePanelTokenDto> GetAllHardwarePanelTokens();

        HardwarePanelTokenDto RegisterHardwarePanel(DeviceRegistrationRequestDto deviceRegistrationRequest);

        //Panels
        IList<HardwarePanelOverviewDto> GetAllHardwarePanels();

        HardwarePanelDto GetHardwarePanelDetails(int id);

        HardwarePanelDto AddHardwarePanel(AddHardwarePanelDto hardwarePanelDto);
        void Delete(int id);
    }
}