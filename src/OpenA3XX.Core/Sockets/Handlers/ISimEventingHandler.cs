using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Sockets.Handlers
{
    public interface ISimEventingHandler
    {
        Task Handle(ISimulatorEventsRepository simulatorEventsRepository, WebSocket webSocket);
    }
}