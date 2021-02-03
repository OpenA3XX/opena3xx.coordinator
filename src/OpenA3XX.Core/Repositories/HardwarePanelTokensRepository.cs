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
            return Find(c => c.DeviceToken == hardwarePanelToken.ToString());
        }

        public HardwarePanelToken GetByHardwarePanelId(int id)
        {
            return Get(id);
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
            var session =  Find(c => c.DeviceToken == hardwarePanelToken.ToString());
            session.LastSeen = DateTime.Now;
            Update(session, session.Id);
            return session;
        }
    }

    public interface IHardwarePanelTokensRepository
    {
        HardwarePanelToken GetByHardwarePanelToken(Guid hardwarePanelToken);
        HardwarePanelToken GetByHardwarePanelId(int id);
        IList<HardwarePanelToken> GetAllHardwarePanelTokens();
        HardwarePanelToken SaveHardwarePanelToken(HardwarePanelToken hardwarePanelToken);

        HardwarePanelToken UpdateLastSeenForHardwarePanel(Guid hardwarePanelToken);

    }
}