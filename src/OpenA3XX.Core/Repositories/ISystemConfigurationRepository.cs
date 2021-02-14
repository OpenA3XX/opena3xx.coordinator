using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface ISystemConfigurationRepository
    {
        IList<SystemConfiguration> GetAllConfiguration();
    }
}