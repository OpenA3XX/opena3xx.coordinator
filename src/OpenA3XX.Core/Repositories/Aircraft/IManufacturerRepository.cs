using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Aircraft
{
    public interface IManufacturerRepository
    {
        IList<Manufacturer> GetAllManufacturers();
        Manufacturer Add(Manufacturer manufacturer);

        /// <summary>
        /// Finds a manufacturer by its name.
        /// </summary>
        /// <param name="name">The manufacturer name to search for.</param>
        /// <returns>The manufacturer if found; otherwise, null.</returns>
        Manufacturer GetByName(string name);
    }
}