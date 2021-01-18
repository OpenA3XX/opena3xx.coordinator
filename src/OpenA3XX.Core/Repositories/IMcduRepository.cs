using OpenA3XX.Core.Enums;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IMcduRepository
    {
        Mcdu Get(CockpitSide cockpitSide);
    }
}