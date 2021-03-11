using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

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
            return FindBy(c => c.Id == hardwareInputSelectorId)
                .Include(c => c.SimulatorEvent).First();
        }

        public HardwareInputSelector UpdateHardwareInputSelector(HardwareInputSelector hardwareInputSelector)
        {
            return Update(hardwareInputSelector, hardwareInputSelector.Id);
        }
    }
}