using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Repositories.Extensions;

namespace OpenA3XX.Core.Repositories
{
    /// <summary>
    /// Repository for managing HardwarePanel entities
    /// </summary>
    public class HardwarePanelRepository : BaseRepository<HardwarePanel>, IHardwarePanelRepository
    {
        /// <summary>
        /// Initializes a new instance of the HardwarePanelRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public HardwarePanelRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Adds a new hardware panel
        /// </summary>
        /// <param name="hardwarePanel">The hardware panel to add</param>
        /// <returns>The added hardware panel</returns>
        public HardwarePanel AddHardwarePanel(HardwarePanel hardwarePanel)
        {
            var result = Add(hardwarePanel);
            Save();
            return result;
        }

        /// <summary>
        /// Gets all hardware panels with basic information (aircraft model and manufacturer)
        /// </summary>
        /// <returns>List of all hardware panels with basic includes</returns>
        public IList<HardwarePanel> GetAllHardwarePanels()
        {
            return GetAll().IncludeBasic().ToList();
        }

        /// <summary>
        /// Gets detailed information for a specific hardware panel including all related entities
        /// </summary>
        /// <param name="id">The hardware panel ID</param>
        /// <returns>The hardware panel with detailed includes</returns>
        public HardwarePanel GetHardwarePanelDetails(int id)
        {
            return FindBy(panel => panel.Id == id)
                .IncludeDetails()
                .First();
        }
    }
}