using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    
    public class HardwarePanelTokensRepository : BaseRepository<HardwarePanelToken>, IHardwarePanelTokensRepository
    {
        public HardwarePanelTokensRepository(DbContext context) : base(context)
        {
        }

        public HardwarePanelToken GetByHardwarePanelToken(Guid hardwarePanelToken)
        {
            return FindBy(c => c.DeviceToken == hardwarePanelToken.ToString()).Include(c => c.HardwarePanel).First();
        }

        public HardwarePanelToken GetByHardwarePanelId(int id)
        {
            return FindBy(c => c.Id == id).Include(c => c.HardwarePanel).First();
        }

        public IList<HardwarePanelToken> GetAllHardwarePanelTokens()
        {
            return GetAll()
                .Include(c => c.HardwarePanel)
                .ToList();
        }

        public HardwarePanelToken SaveHardwarePanelToken(HardwarePanelToken hardwarePanelToken)
        {
            return Add(hardwarePanelToken);
        }

        public HardwarePanelToken UpdateLastSeenForHardwarePanel(Guid hardwarePanelToken)
        {
            var session = FindBy(c => c.DeviceToken == hardwarePanelToken.ToString()).First();
            session.LastSeen = DateTime.Now;
            Update(session, session.Id);
            return session;
        }
    }
}