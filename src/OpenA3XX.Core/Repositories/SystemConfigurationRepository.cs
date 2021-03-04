using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public class SystemConfigurationRepository : BaseRepository<SystemConfiguration>, ISystemConfigurationRepository
    {
        public SystemConfigurationRepository(DbContext context) : base(context)
        {
        }

        public IList<SystemConfiguration> GetAllConfiguration()
        {
            return GetAll().ToList();
        }

        public void UpdateAllConfiguration(IList<SystemConfiguration> systemConfigurationList)
        {
            foreach (var configuration in systemConfigurationList)
            {
                var item = FindBy(c => c.Key == configuration.Key).First();
                configuration.Id = item.Id;
                Update(configuration, item.Id);
            }
        }
    }
}