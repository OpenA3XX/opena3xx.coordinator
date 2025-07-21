using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareOutputTypesRepository : BaseRepository<HardwareOutputType>, IHardwareOutputTypesRepository
    {
        public HardwareOutputTypesRepository(DbContext context, ILogger<BaseRepository<HardwareOutputType>> logger) : base(context, logger)
        {
        }

        public IList<HardwareOutputType> GetAllHardwareOutputTypes()
        {
            return GetAll().ToList();
        }

        public HardwareOutputType GetHardwareOutputTypeBy(int id)
        {
            return Get(id);
        }

        public HardwareOutputType AddHardwareOutputType(HardwareOutputType hardwareOutputType)
        {
            var storedModel = Find(c => c.Name == hardwareOutputType.Name);
            if (storedModel == null) return Add(hardwareOutputType);

            throw new HardwareOutputTypeExistsException(
                $"Hardware Output Type with {hardwareOutputType.Name} already exists");
        }

        public HardwareOutputType UpdateHardwareOutputType(HardwareOutputType hardwareOutputType)
        {
            return Update(hardwareOutputType, hardwareOutputType.Id);
        }
    }
}