using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareOutputSelectorRepository : BaseRepository<HardwareOutputSelector>,
        IHardwareOutputSelectorRepository
    {
        public HardwareOutputSelectorRepository(DbContext context) : base(context)
        {
        }


        public IList<HardwareOutputSelector> GetAllOutputSelectors()
        {
            return GetAll().ToList();
        }

        public HardwareOutputSelector GetHardwareOutputSelectorBy(int hardwareOutputSelectorId)
        {
            return FindBy(c => c.Id == hardwareOutputSelectorId).First();
            //.Include(c => c.SimulatorEvent).First();
        }
        
        

        public HardwareOutputSelector UpdateHardwareOutputSelector(HardwareOutputSelector hardwareInputSelector)
        {
            return Update(hardwareInputSelector, hardwareInputSelector.Id);
        }
    }
}