using System.Text;
using OpenA3XX.Core.Dtos;
using RabbitMQ.Client;

namespace OpenA3XX.Core.Services
{
    public class SimulatorEventingService : ISimulatorEventingService
    {
        private readonly IModel _channel;

        public SimulatorEventingService()
        {
            var factory = new ConnectionFactory
            {
                UserName = "opena3xx",
                Password = "opena3xx",
                VirtualHost = "/",
                HostName = "192.168.50.22",
                ClientProvidedName = "app:opena3xx.peripheral.webapi"
            };

            var conn = factory.CreateConnection();
            _channel = conn.CreateModel();

            _channel.QueueDeclare("simulator_test_events", false, false, false, null);
        }

        public void SendSimulatorTestEvent(SimulatorEventDto simulatorEventDto)
        {
            var payload = Encoding.UTF8.GetBytes(simulatorEventDto.EventCode);

            _channel.BasicPublish("",
                "simulator_test_events",
                null,
                payload);
        }
    }
}