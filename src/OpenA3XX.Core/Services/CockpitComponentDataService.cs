using OpenA3XX.Core.Enums;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class CockpitComponentDataService : ICockpitComponentDataService
    {
        public ICockpitComponent GetCockpitComponent(CockpitSide cockpitSide, CockpitComponent cockpitComponent)
        {
            switch (cockpitComponent)
            {
                case CockpitComponent.Mcdu:
                    var mcduRepository = new McduRepository();
                    var mcdu = mcduRepository.Get(cockpitSide);
                    return mcdu;
            }

            return null;
        }
    }
}