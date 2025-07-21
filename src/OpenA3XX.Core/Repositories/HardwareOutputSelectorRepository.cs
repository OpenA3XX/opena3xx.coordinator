using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareOutputSelectorRepository : BaseRepository<HardwareOutputSelector>,
        IHardwareOutputSelectorRepository
    {
        public HardwareOutputSelectorRepository(DbContext context, ILogger<BaseRepository<HardwareOutputSelector>> logger) : base(context, logger)
        {
        }


        public IList<HardwareOutputSelector> GetAllOutputSelectors()
        {
            return GetAll().ToList();
        }

        public HardwareOutputSelector GetHardwareOutputSelectorBy(int hardwareOutputSelectorId)
        {
            Logger.LogInformation("Getting hardware output selector by ID: {HardwareOutputSelectorId}", hardwareOutputSelectorId);
            
            var result = FindBy(c => c.Id == hardwareOutputSelectorId).FirstOrDefault();
            
            if (result == null)
            {
                Logger.LogWarning("Hardware output selector with ID {HardwareOutputSelectorId} not found", hardwareOutputSelectorId);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved hardware output selector with ID {HardwareOutputSelectorId}", hardwareOutputSelectorId);
            }
            
            return result;
        }
        
        

        public HardwareOutputSelector UpdateHardwareOutputSelector(HardwareOutputSelector hardwareInputSelector)
        {
            return Update(hardwareInputSelector, hardwareInputSelector.Id);
        }
    }
}