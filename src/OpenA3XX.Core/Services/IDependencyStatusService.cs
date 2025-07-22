using System.Threading.Tasks;
using OpenA3XX.Core.Dtos;

namespace OpenA3XX.Core.Services
{
    /// <summary>
    /// Service for checking the status of system dependencies
    /// </summary>
    public interface IDependencyStatusService
    {
        /// <summary>
        /// Checks the status of all system dependencies
        /// </summary>
        /// <returns>Dependency status information</returns>
        Task<DependencyStatusDto> GetDependencyStatusAsync();
        
        /// <summary>
        /// Checks if Microsoft Flight Simulator is running and accessible
        /// </summary>
        /// <returns>True if MSFS is running, false otherwise</returns>
        Task<DependencyDetailDto> CheckMsfsStatusAsync();
        
        /// <summary>
        /// Checks if RabbitMQ is running and accessible
        /// </summary>
        /// <returns>True if RabbitMQ is running, false otherwise</returns>
        Task<DependencyDetailDto> CheckRabbitMqStatusAsync();
        
        /// <summary>
        /// Checks if SEQ logging service is running and accessible
        /// </summary>
        /// <returns>True if SEQ is running, false otherwise</returns>
        Task<DependencyDetailDto> CheckSeqStatusAsync();
    }
} 