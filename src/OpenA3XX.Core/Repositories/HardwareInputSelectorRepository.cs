using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareInputSelectorRepository : BaseRepository<HardwareInputSelector>,
        IHardwareInputSelectorRepository
    {
        public HardwareInputSelectorRepository(DbContext context, ILogger<BaseRepository<HardwareInputSelector>> logger) : base(context, logger)
        {
        }


        public IList<HardwareInputSelector> GetAllInputSelectors()
        {
            return GetAll().ToList();
        }

        public HardwareInputSelector GetHardwareInputSelectorBy(int hardwareInputSelectorId)
        {
            Logger.LogInformation("Getting hardware input selector by ID: {HardwareInputSelectorId}", hardwareInputSelectorId);
            
            var result = FindBy(selector => selector.Id == hardwareInputSelectorId)
                .IncludeSimulatorEvent()
                .FirstOrDefault();
                
            if (result == null)
            {
                Logger.LogWarning("Hardware input selector with ID {HardwareInputSelectorId} not found", hardwareInputSelectorId);
            }
            else
            {
                Logger.LogInformation("Successfully retrieved hardware input selector with ID {HardwareInputSelectorId}", hardwareInputSelectorId);
            }
            
            return result;
        }
        
        

        public HardwareInputSelector UpdateHardwareInputSelector(HardwareInputSelector hardwareInputSelector)
        {
            return Update(hardwareInputSelector, hardwareInputSelector.Id);
        }
    }
}