using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;

namespace Opena3XX.Eventing.Msfs
{
    public class MessageResponse
    {
        public int input_selector_id { get; set; }
        
    }
    public class HardwareInputSelectorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SimulatorEventDto SimulatorEventDto { get; set; }
    }
    
    public class SimulatorEventDto
    {
        public int Id { get; set; }

        public string FriendlyName { get; set; }

        public string EventName { get; set; }

        public string SimulatorEventTypeName { get; set; }

        public string SimulatorSoftwareName { get; set; }

        public string SimulatorEventSdkTypeName { get; set; }

        public string EventCode { get; set; }
    }
    
    
    internal static class Program
    {
        private static FsConnect _fsConnect;

        public static async Task Main(string[] args)
        {
            try
            {
                _fsConnect = new FsConnect();

                try
                {
                    Console.WriteLine($"Connecting to Flight Simulator on 127.0.0.1:500");
                    _fsConnect.Connect("FsConnectTestConsole", "127.0.0.1" , 500, SimConnectProtocol.Ipv4);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                var factory = new ConnectionFactory
                {
                    UserName = "opena3xx",
                    Password = "opena3xx",
                    VirtualHost = "/",
                    HostName = "192.168.50.22",
                    ClientProvidedName = "app:opena3xx.eventing.msfs component:simulator_test_events"
                };
                
                _fsConnect.SetText("OpenA3XX Sim Connector: Connected", 5);

                var conn = await factory.CreateConnectionAsync();
                var channel = await conn.CreateChannelAsync();
                
                var consumer = new AsyncEventingBasicConsumer(channel);
                
                consumer.ReceivedAsync += async (ch, ea) =>
                {
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                    
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var messageObj = JsonConvert.DeserializeObject<MessageResponse>(message);
                    
                    var client = new RestClient("http://192.168.50.22:5000");
                    var request = new RestRequest($"hardware-input-selectors/{messageObj.input_selector_id}", Method.Get);
                    var response = await client.ExecuteAsync<HardwareInputSelectorDto>(request);
                    var dto = response.Data;
                    
                    var eventCode = dto.SimulatorEventDto.EventCode.Split('#')[0];
                    Console.WriteLine(eventCode);
                    _fsConnect.SetEventId(eventCode);
                };

                await channel.BasicConsumeAsync("processor-msfs", false, consumer);
                
                Console.ReadKey();
                Console.WriteLine("Disconnecting from Flight Simulator");
                _fsConnect.SetText("OpenA3XX Sim Connector: Disconnected", 1);
                _fsConnect.Disconnect();
                _fsConnect.Dispose();
                _fsConnect = null;
                
                await channel.CloseAsync();
                await conn.CloseAsync();

                Console.WriteLine("Done");
                
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e);
            }
        }
    }
}