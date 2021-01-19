using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenA3XX.Core.Eventing;
using Serilog;

namespace OpenA3XX.Core.Sockets.Handlers
{
    public class FlightSimulatorEventingHandler : ISimEventingHandler
    {
        public async Task Handle(WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult socketResponse;
                var package = new List<byte>();
                do
                {
                    socketResponse = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    package.AddRange(new ArraySegment<byte>(buffer, 0, socketResponse.Count));
                } while (!socketResponse.EndOfMessage);
                var bufferAsString = Encoding.ASCII.GetString(package.ToArray());
                if (!string.IsNullOrEmpty(bufferAsString))
                {
                    if (!bufferAsString.Contains("handshake") && !bufferAsString.Contains("interaction"))
                    {
                        var data = JsonConvert.DeserializeObject<FlightSimulatorEvent>(bufferAsString);
                        
                        
                        Log.Information("{@data}", data);    
                    }
                }
            }
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            
        }
    }
}