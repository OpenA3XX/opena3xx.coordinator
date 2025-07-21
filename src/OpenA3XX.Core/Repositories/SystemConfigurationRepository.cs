using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public class SystemConfigurationRepository : BaseRepository<SystemConfiguration>, ISystemConfigurationRepository
    {
        public SystemConfigurationRepository(DbContext context, ILogger<BaseRepository<SystemConfiguration>> logger) : base(context, logger)
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
                // Use FirstOrDefault to avoid "Sequence contains no elements" exception
                var existingItem = FindBy(c => c.Key == configuration.Key).FirstOrDefault();
                
                if (existingItem != null)
                {
                    // Update existing configuration
                    configuration.Id = existingItem.Id;
                    Update(configuration, existingItem.Id);
                }
                else
                {
                    // Log missing configuration key for troubleshooting
                    Logger.LogWarning("Configuration key '{ConfigKey}' not found in database during UpdateAllConfiguration. Skipping update.", 
                        configuration.Key);
                }
            }
            
            // Save all changes after processing all configurations
            Save();
        }
    }
}