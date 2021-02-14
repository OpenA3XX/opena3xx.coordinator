using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IManufacturerRepository
    {
        IList<Manufacturer> GetAllManufacturers();
    }
}