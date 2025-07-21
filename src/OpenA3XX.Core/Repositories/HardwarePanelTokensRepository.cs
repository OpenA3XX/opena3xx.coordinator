using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    public class HardwarePanelTokensRepository : BaseRepository<HardwarePanelToken>, IHardwarePanelTokensRepository
    {
        public HardwarePanelTokensRepository(DbContext context, ILogger<BaseRepository<HardwarePanelToken>> logger) : base(context, logger)
        {
        }

        public HardwarePanelToken GetByHardwarePanelToken(Guid hardwarePanelToken)
        {
            Logger.LogInformation("Getting hardware panel token by token: {HardwarePanelToken}", hardwarePanelToken);
            
            var result = FindBy(token => token.DeviceToken == hardwarePanelToken.ToString())
                .IncludeHardwarePanel()
                .FirstOrDefault();
                
            if (result == null)
            {
                Logger.LogWarning("Hardware panel token {HardwarePanelToken} not found", hardwarePanelToken);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved hardware panel token {HardwarePanelToken}", hardwarePanelToken);
            }
            
            return result;
        }

        public HardwarePanelToken GetByHardwarePanelId(int id)
        {
            Logger.LogInformation("Getting hardware panel token by panel ID: {HardwarePanelId}", id);
            
            var result = FindBy(token => token.Id == id)
                .IncludeHardwarePanel()
                .FirstOrDefault();
                
            if (result == null)
            {
                Logger.LogWarning("Hardware panel token with panel ID {HardwarePanelId} not found", id);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved hardware panel token with panel ID {HardwarePanelId}", id);
            }
            
            return result;
        }

        public IList<HardwarePanelToken> GetAllHardwarePanelTokens()
        {
            return GetAll()
                .IncludeHardwarePanel()
                .ToList();
        }

        public HardwarePanelToken SaveHardwarePanelToken(HardwarePanelToken hardwarePanelToken)
        {
            return Add(hardwarePanelToken);
        }

        public HardwarePanelToken UpdateLastSeenForHardwarePanel(Guid hardwarePanelToken)
        {
            Logger.LogInformation("Updating last seen for hardware panel token: {HardwarePanelToken}", hardwarePanelToken);
            
            var session = FindBy(c => c.DeviceToken == hardwarePanelToken.ToString()).FirstOrDefault();
            
            if (session == null)
            {
                Logger.LogWarning("Hardware panel token {HardwarePanelToken} not found for last seen update", hardwarePanelToken);
                return null;
            }
            
            session.LastSeen = DateTime.Now;
            Update(session, session.Id);
            Save();
            
            Logger.LogInformation("Successfully updated last seen for hardware panel token {HardwarePanelToken}", hardwarePanelToken);
            return session;
        }
    }
}