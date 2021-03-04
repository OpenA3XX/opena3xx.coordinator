namespace OpenA3XX.Core.Sockets.Handlers
{
    public class FlightSimulatorEventingHandler : ISimEventingHandler
    {
        /*
        public async Task Handle(ISimulatorEventsRepository simulatorEventsRepository, WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult socketResponse;
                var package = new List<byte>();
                do
                {
                    socketResponse =
                        await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    package.AddRange(new ArraySegment<byte>(buffer, 0, socketResponse.Count));
                } while (!socketResponse.EndOfMessage);

                var bufferAsString = Encoding.ASCII.GetString(package.ToArray());
                if (!string.IsNullOrEmpty(bufferAsString))
                {
                    if (!bufferAsString.Contains("handshake") && !bufferAsString.Contains("interaction"))
                    {
                        var data = JsonConvert.DeserializeObject<FlightSimulatorEvent>(bufferAsString);

                        var allEvents = simulatorEventsRepository.GetAll();
                        
                        
                        
                        
                        /*
                        var factory = new ConnectionFactory {HostName = "localhost"};
                        using (var connection = factory.CreateConnection())
                        using (var channel = connection.CreateModel())
                        {
                            channel.ExchangeDeclare(exchange: "simulator_events_exchange", type: ExchangeType.Fanout);

                            var body = Encoding.UTF8.GetBytes(bufferAsString);
                            channel.BasicPublish("simulator_events_exchange", "", null, body);
                        }**\


                        Log.Information("{@data}", data);
                    }
                }
            }

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }*/
    }
}