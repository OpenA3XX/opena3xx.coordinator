using System.Collections.Generic;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    public interface IHardwareInputTypeService
    {
        IList<HardwareInputTypeDto> GetAll();

        HardwareInputTypeDto Add(HardwareInputTypeDto hardwareInputTypeDto);

        HardwareInputTypeDto Update(HardwareInputTypeDto hardwareInputTypeDto);
    }
}