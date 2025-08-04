using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using OpenA3XX.Core.Repositories.Simulation;

namespace OpenA3XX.Core.Sockets.Handlers
{
    /// <summary>
    /// Interface for handling flight simulator events via WebSocket connections
    /// </summary>
    public interface ISimEventingHandler
    {
        /// <summary>
        /// Handles WebSocket communication for simulator events
        /// </summary>
        /// <param name="simulatorEventsRepository">Repository for simulator events</param>
        /// <param name="webSocket">The WebSocket connection</param>
        /// <param name="cancellationToken">Cancellation token for graceful shutdown</param>
        /// <returns>Task representing the handling operation</returns>
        Task Handle(ISimulatorEventRepository simulatorEventsRepository, WebSocket webSocket, CancellationToken cancellationToken = default);
    }
}