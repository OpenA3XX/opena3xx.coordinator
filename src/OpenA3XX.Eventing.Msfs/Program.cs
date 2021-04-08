using System;
using System.Text;
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

        public static void Main(string[] args)
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

                var conn = factory.CreateConnection();
                var channel = conn.CreateModel();
                /*
                channel.QueueDeclare("simulator_test_events", false, false, false, null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, ea) =>
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    var calculatorCode = Encoding.UTF8.GetString(ea.Body.ToArray());
                    _fsConnect.SetEventId(calculatorCode);
                };

                var consumerTag = channel.BasicConsume("simulator_test_events", false, consumer);
                */
                
                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += (ch, ea) =>
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var messageObj = JsonConvert.DeserializeObject<MessageResponse>(message);
                    
                    var client = new RestClient("http://192.168.50.22:5000");
                    var dto = client.Get<HardwareInputSelectorDto>(new RestRequest($"hardware-input-selectors/{messageObj.input_selector_id}", Method.GET)).Data;
                    Console.WriteLine(dto.SimulatorEventDto.EventCode.Split('#')[0]);
                    _fsConnect.SetEventId(dto.SimulatorEventDto.EventCode.Split('#')[0]);
                };

                var consumerTag = channel.BasicConsume("processor-msfs", false, consumer);
                
                Console.ReadKey();
                Console.WriteLine("Disconnecting from Flight Simulator");
                _fsConnect.SetText("OpenA3XX Sim Connector: Disconnected", 1);
                _fsConnect.Disconnect();
                _fsConnect.Dispose();
                _fsConnect = null;
                
                channel.Dispose();
                conn.Dispose();

                Console.WriteLine("Done");
                
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e);
            }
        }
        
        
    }
}