using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenA3XX.Core.Eventing;
using OpenA3XX.Core.Repositories.Simulation;

namespace OpenA3XX.Core.Sockets.Handlers
{
    public class FlightSimulatorEventingHandler : ISimEventingHandler
    {
        
        public async Task Handle(ISimulatorEventRepository simulatorEventsRepository, WebSocket webSocket, CancellationToken cancellationToken = default)
        {
            const int bufferSize = 1024 * 4;
            
            try
            {
                while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
                {
                    var buffer = new byte[bufferSize];
                    WebSocketReceiveResult socketResponse;
                    var package = new List<byte>();
                    
                    do
                    {
                        socketResponse = await webSocket.ReceiveAsync(
                            new ArraySegment<byte>(buffer), 
                            cancellationToken);
                        package.AddRange(new ArraySegment<byte>(buffer, 0, socketResponse.Count));
                    } while (!socketResponse.EndOfMessage && !cancellationToken.IsCancellationRequested);

                    if (socketResponse.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }

                    var bufferAsString = Encoding.UTF8.GetString(package.ToArray());
                    if (!string.IsNullOrEmpty(bufferAsString))
                    {
                        await ProcessMessage(bufferAsString, simulatorEventsRepository);
                    }
                }
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                // Expected cancellation - no need to log as error
            }
            catch (WebSocketException ex) when (ex.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
            {
                // Connection closed by client - normal scenario
            }
            finally
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    try
                    {
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.NormalClosure, 
                            "Handler completed", 
                            CancellationToken.None);
                    }
                    catch (WebSocketException)
                    {
                        // Connection already closed - ignore
                    }
                }
            }
        }

        /// <summary>
        /// Processes a received WebSocket message
        /// </summary>
        /// <param name="message">The message content</param>
        /// <param name="simulatorEventsRepository">The simulator events repository</param>
        private async Task ProcessMessage(string message, ISimulatorEventRepository simulatorEventsRepository)
        {
            try
            {
                if (message.Contains("handshake") || message.Contains("interaction"))
                {
                    return; // Skip handshake and interaction messages
                }

                var data = JsonConvert.DeserializeObject<FlightSimulatorEvent>(message);
                if (data != null)
                {
                    // Process the flight simulator event
                    // TODO: Implement event processing logic
                    await Task.CompletedTask; // Placeholder for async processing
                }
            }
            catch (JsonException)
            {
                // Invalid JSON - ignore
            }
            catch (Exception)
            {
                // Log other exceptions if needed but don't crash the handler
            }
        }
    }
}