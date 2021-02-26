using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareInputTypesRepository : BaseRepository<HardwareInputType>, IHardwareInputTypesRepository
    {
        public HardwareInputTypesRepository(DbContext context) : base(context)
        {
        }
        
        public IList<HardwareInputType> GetAllHardwareInputTypes()
        {
            return GetAll().ToList();
        }

        public HardwareInputType GetHardwareInputTypeBy(int id)
        {
            return Get(id);
        }

        public HardwareInputType AddHardwareInputType(HardwareInputType hardwareInputType)
        {
            var storedModel = Find(c => c.Name == hardwareInputType.Name);
            if (storedModel == null)
            {
                return Add(hardwareInputType);
            }

            throw new HardwareInputTypeExistsException(
                $"Hardware Input Type with {hardwareInputType.Name} already exists");
        }

        public HardwareInputType UpdateHardwareInputType(HardwareInputType hardwareInputType)
        {
            return Update(hardwareInputType, hardwareInputType.Id);
        }
    }
}