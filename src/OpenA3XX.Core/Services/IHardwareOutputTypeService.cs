using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwareOutputTypeService
    {
        IList<HardwareOutputTypeDto> GetAll();

        HardwareOutputTypeDto GetBy(int id);

        HardwareOutputTypeDto Add(HardwareOutputTypeDto hardwareInputTypeDto);

        HardwareOutputTypeDto Update(HardwareOutputTypeDto hardwareInputTypeDto);
        void Delete(int id);
    }
}