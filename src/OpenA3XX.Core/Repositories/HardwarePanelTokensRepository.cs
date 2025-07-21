using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    public class HardwarePanelTokensRepository : BaseRepository<HardwarePanelToken>, IHardwarePanelTokensRepository
    {
        public HardwarePanelTokensRepository(DbContext context) : base(context)
        {
        }

        public HardwarePanelToken GetByHardwarePanelToken(Guid hardwarePanelToken)
        {
            return FindBy(token => token.DeviceToken == hardwarePanelToken.ToString())
                .IncludeHardwarePanel()
                .First();
        }

        public HardwarePanelToken GetByHardwarePanelId(int id)
        {
            return FindBy(token => token.Id == id)
                .IncludeHardwarePanel()
                .First();
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
            var session = FindBy(c => c.DeviceToken == hardwarePanelToken.ToString()).First();
            session.LastSeen = DateTime.Now;
            Update(session, session.Id);
            return session;
        }
    }
}