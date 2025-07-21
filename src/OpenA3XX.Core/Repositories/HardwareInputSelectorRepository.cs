using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareInputSelectorRepository : BaseRepository<HardwareInputSelector>,
        IHardwareInputSelectorRepository
    {
        public HardwareInputSelectorRepository(DbContext context) : base(context)
        {
        }


        public IList<HardwareInputSelector> GetAllInputSelectors()
        {
            return GetAll().ToList();
        }

        public HardwareInputSelector GetHardwareInputSelectorBy(int hardwareInputSelectorId)
        {
            return FindBy(selector => selector.Id == hardwareInputSelectorId)
                .IncludeSimulatorEvent()
                .First();
        }
        
        

        public HardwareInputSelector UpdateHardwareInputSelector(HardwareInputSelector hardwareInputSelector)
        {
            return Update(hardwareInputSelector, hardwareInputSelector.Id);
        }
    }
}