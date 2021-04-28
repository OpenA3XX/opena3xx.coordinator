using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwareOutputSelectorService
    {
        HardwareOutputSelectorDto GetHardwareOutputSelectorDetails(int hardwareOutputSelectorId);
    }
}