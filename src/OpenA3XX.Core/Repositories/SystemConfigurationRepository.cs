using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;

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
    }

    public interface ISystemConfigurationRepository
    {
        IList<SystemConfiguration> GetAllConfiguration();
    }
}