using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Extensions
{
    /// <summary>
    /// Extension methods for Entity Framework Include operations to reduce code duplication
    /// </summary>
    public static class IncludeExtensions
    {
        /// <summary>
        /// Includes the complete hardware board hierarchy with all related entities
        /// </summary>
        /// <param name="query">The queryable to extend</param>
        /// <returns>Queryable with all hardware board includes</returns>
        public static IQueryable<HardwareBoard> IncludeFullHierarchy(this IQueryable<HardwareBoard> query)
        {
            return query
                .Include(board => board.Buses)
                    .ThenInclude(bus => bus.Bits)
                        .ThenInclude(bit => bit.HardwareInputSelector)
                            .ThenInclude(selector => selector.HardwareInput)
                                .ThenInclude(input => input.HardwareInputType)
                .Include(board => board.Buses)
                    .ThenInclude(bus => bus.Bits)
                        .ThenInclude(bit => bit.HardwareInputSelector)
                            .ThenInclude(selector => selector.HardwareInput)
                                .ThenInclude(input => input.HardwarePanel)
                .Include(board => board.Buses)
                    .ThenInclude(bus => bus.Bits)
                        .ThenInclude(bit => bit.HardwareOutputSelector)
                            .ThenInclude(selector => selector.HardwareOutput)
                                .ThenInclude(output => output.HardwareOutputType)
                .Include(board => board.Buses)
                    .ThenInclude(bus => bus.Bits)
                        .ThenInclude(bit => bit.HardwareOutputSelector)
                            .ThenInclude(selector => selector.HardwareOutput)
                                .ThenInclude(output => output.HardwarePanel);
        }

        /// <summary>
        /// Includes hardware panel with its related entities
        /// </summary>
        /// <param name="query">The queryable to extend</param>
        /// <returns>Queryable with hardware panel includes</returns>
        public static IQueryable<HardwarePanel> IncludeDetails(this IQueryable<HardwarePanel> query)
        {
            return query
                .Include(panel => panel.AircraftModel)
                    .ThenInclude(model => model.Manufacturer)
                .Include(panel => panel.HardwareInput)
                    .ThenInclude(input => input.HardwareInputType)
                .Include(panel => panel.HardwareInput)
                    .ThenInclude(input => input.HardwareInputSelectorList)
                        .ThenInclude(selector => selector.SimulatorEvent)
                .Include(panel => panel.HardwareOutput)
                    .ThenInclude(output => output.HardwareOutputType)
                .Include(panel => panel.HardwareOutput)
                    .ThenInclude(output => output.HardwareOutputSelectorList);
        }

        /// <summary>
        /// Includes basic hardware panel information
        /// </summary>
        /// <param name="query">The queryable to extend</param>
        /// <returns>Queryable with basic hardware panel includes</returns>
        public static IQueryable<HardwarePanel> IncludeBasic(this IQueryable<HardwarePanel> query)
        {
            return query
                .Include(panel => panel.AircraftModel)
                    .ThenInclude(model => model.Manufacturer);
        }

        /// <summary>
        /// Includes manufacturer with aircraft models and hardware panels
        /// </summary>
        /// <param name="query">The queryable to extend</param>
        /// <returns>Queryable with manufacturer includes</returns>
        public static IQueryable<Manufacturer> IncludeHierarchy(this IQueryable<Manufacturer> query)
        {
            return query
                .Include(manufacturer => manufacturer.AircraftModels)
                    .ThenInclude(model => model.HardwarePanels)
                        .ThenInclude(panel => panel.HardwareInput)
                            .ThenInclude(input => input.HardwareInputType)
                .Include(manufacturer => manufacturer.AircraftModels)
                    .ThenInclude(model => model.HardwarePanels)
                        .ThenInclude(panel => panel.HardwareOutput)
                            .ThenInclude(output => output.HardwareOutputType);
        }

        /// <summary>
        /// Includes hardware panel token with related hardware panel
        /// </summary>
        /// <param name="query">The queryable to extend</param>
        /// <returns>Queryable with hardware panel token includes</returns>
        public static IQueryable<HardwarePanelToken> IncludeHardwarePanel(this IQueryable<HardwarePanelToken> query)
        {
            return query.Include(token => token.HardwarePanel);
        }

        /// <summary>
        /// Includes hardware input selector with related simulator event
        /// </summary>
        /// <param name="query">The queryable to extend</param>
        /// <returns>Queryable with hardware input selector includes</returns>
        public static IQueryable<HardwareInputSelector> IncludeSimulatorEvent(this IQueryable<HardwareInputSelector> query)
        {
            return query.Include(selector => selector.SimulatorEvent);
        }

        /// <summary>
        /// Includes aircraft model with manufacturer
        /// </summary>
        /// <param name="query">The queryable to extend</param>
        /// <returns>Queryable with aircraft model includes</returns>
        public static IQueryable<AircraftModel> IncludeManufacturer(this IQueryable<AircraftModel> query)
        {
            return query.Include(model => model.Manufacturer);
        }
    }
} 