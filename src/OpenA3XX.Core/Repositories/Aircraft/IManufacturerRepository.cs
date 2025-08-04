using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Aircraft
{
    public interface IManufacturerRepository
    {
        IList<Manufacturer> GetAllManufacturers();
        Manufacturer Add(Manufacturer manufacturer);
    }
}