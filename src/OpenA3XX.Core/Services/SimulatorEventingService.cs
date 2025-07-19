using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using RabbitMQ.Client;

namespace OpenA3XX.Core.Services
{
    public class SimulatorEventingService : ISimulatorEventingService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;  // Changed from IModel to IChannel
        private readonly ILogger<SimulatorEventingService> _logger;
        private readonly object _channelLock = new object();
        private bool _disposed = false;

        public SimulatorEventingService(IConfiguration configuration, ILogger<SimulatorEventingService> logger)
        {
            _logger = logger;
            
            var factory = new ConnectionFactory
            {
                UserName = configuration["RabbitMQ:Username"] ?? "guest",
                Password = configuration["RabbitMQ:Password"] ?? "guest",
                VirtualHost = configuration["RabbitMQ:VirtualHost"] ?? "/",
                HostName = configuration["RabbitMQ:HostName"] ?? "localhost",
                ClientProvidedName = "app:opena3xx.peripheral.webapi"
            };
            
            try
            {
                _connection = factory.CreateConnectionAsync().Result;
                _channel = _connection.CreateChannelAsync().Result;
                _channel.QueueDeclareAsync("simulator_test_events", false, false, false, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize RabbitMQ connection");
                throw;
            }
        }

        public void SendSimulatorTestEvent(SimulatorEventDto simulatorEventDto)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SimulatorEventingService));
            
            if (simulatorEventDto?.EventName == null)
            {
                _logger.LogWarning("Attempted to send null or invalid simulator event");
                return;
            }

            try
            {
                var payload = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(simulatorEventDto));
                
                lock (_channelLock)
                {
                    _channel?.BasicPublishAsync("",
                        "simulator_test_events",
                        payload);
                }
                
                _logger.LogDebug("Simulator event sent: {EventName}", simulatorEventDto.EventName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send simulator event: {EventName}", simulatorEventDto.EventName);
                throw;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _channel?.CloseAsync();
                _channel?.Dispose();
                _connection?.CloseAsync();
                _connection?.Dispose();
                _disposed = true;
            }
        }
    }
}