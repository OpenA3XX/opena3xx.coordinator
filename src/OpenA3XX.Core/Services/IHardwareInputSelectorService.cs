using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwareInputSelectorService
    {
        HardwareInputSelectorDto GetHardwareInputSelectorDetails(int hardwareInputSelectorId);
        HardwareInputSelectorDto Add(AddHardwareInputSelectorDto addHardwareInputSelectorDto);
        void Delete(int id);
    }
}