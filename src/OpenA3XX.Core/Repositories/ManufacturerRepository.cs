using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    /// <summary>
    /// Repository for managing Manufacturer entities
    /// </summary>
    public class ManufacturerRepository : BaseRepository<Manufacturer>, IManufacturerRepository
    {
        /// <summary>
        /// Initializes a new instance of the ManufacturerRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public ManufacturerRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all manufacturers with their complete hierarchy of related entities
        /// </summary>
        /// <returns>List of all manufacturers with full includes</returns>
        public IList<Manufacturer> GetAllManufacturers()
        {
            return GetAll()
                .IncludeHierarchy()
                .ToList();
        }
    }
}