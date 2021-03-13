using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Opena3XX.Eventing.Msfs
{
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

                channel.QueueDeclare("simulator_test_events", false, false, false, null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, ea) =>
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    var calculatorCode = Encoding.UTF8.GetString(ea.Body.ToArray());
                    _fsConnect.SetEventId(calculatorCode);
                };

                var consumerTag = channel.BasicConsume("simulator_test_events", false, consumer);

                Console.ReadKey();
                Console.WriteLine("Disconnecting from Flight Simulator");
                _fsConnect.SetText("Test Console disconnecting", 1);
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