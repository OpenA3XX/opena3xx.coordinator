using OpenA3XX.Core.Enums;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Services
{
    public interface ICockpitComponentDataService
    {
        ICockpitComponent GetCockpitComponent(CockpitSide cockpitSide, CockpitComponent cockpitComponent);
    }
}