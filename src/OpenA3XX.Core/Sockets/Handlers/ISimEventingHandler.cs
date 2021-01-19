using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OpenA3XX.Core.Sockets.Handlers
{
    public interface ISimEventingHandler
    {
        Task Handle(WebSocket webSocket);
    }
}