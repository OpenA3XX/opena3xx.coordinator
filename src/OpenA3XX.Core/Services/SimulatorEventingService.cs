using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Dtos;
using RabbitMQ.Client;

namespace OpenA3XX.Core.Services
{
    public class SimulatorEventingService : ISimulatorEventingService, IDisposable
    {
        private IConnection _connection;
        private IChannel _channel;  // Changed from IModel to IChannel
        private readonly ILogger<SimulatorEventingService> _logger;
        private readonly object _channelLock = new object();
        private bool _disposed = false;

        public SimulatorEventingService(IOptions<RabbitMQOptions> rabbitMQOptions, ILogger<SimulatorEventingService> logger)
        {
            _logger = logger;
            var options = rabbitMQOptions.Value;
            
            var factory = new ConnectionFactory
            {
                UserName = options.Username,
                Password = options.Password,
                VirtualHost = options.VirtualHost,
                HostName = options.HostName,
                Port = options.Port,
                RequestedConnectionTimeout = TimeSpan.FromSeconds(options.ConnectionTimeout),
                SocketReadTimeout = TimeSpan.FromSeconds(options.SocketReadTimeout),
                SocketWriteTimeout = TimeSpan.FromSeconds(options.SocketWriteTimeout),
                ClientProvidedName = options.ClientProvidedName
            };
            
            // Initialize connection synchronously in constructor, but could be improved by moving to factory pattern
            InitializeConnectionAsync(factory, options).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Initializes the RabbitMQ connection asynchronously
        /// </summary>
        private async Task InitializeConnectionAsync(ConnectionFactory factory, RabbitMQOptions options)
        {
            try
            {
                _logger.LogInformation("Connecting to RabbitMQ at {HostName}:{Port}", options.HostName, options.Port);
                _connection = await factory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();
                await _channel.QueueDeclareAsync(options.Queues.SimulatorTestEvents, false, false, false, null);
                _logger.LogInformation("RabbitMQ connection established successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize RabbitMQ connection to {HostName}:{Port}", options.HostName, options.Port);
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

            // Fire and forget pattern - don't await to maintain synchronous interface
            _ = SendSimulatorTestEventAsync(simulatorEventDto);
        }

        /// <summary>
        /// Sends a simulator test event asynchronously
        /// </summary>
        /// <param name="simulatorEventDto">The simulator event to send</param>
        /// <returns>Task representing the send operation</returns>
        public async Task SendSimulatorTestEventAsync(SimulatorEventDto simulatorEventDto)
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
                
                // Use async version for better resource utilization
                if (_channel != null)
                {
                    await _channel.BasicPublishAsync("",
                        "simulator_test_events", // TODO: Make this configurable
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
                try
                {
                    // Use GetAwaiter().GetResult() for synchronous disposal
                    if (_channel != null)
                    {
                        _channel.CloseAsync().GetAwaiter().GetResult();
                        _channel.Dispose();
                    }
                    
                    if (_connection != null)
                    {
                        _connection.CloseAsync().GetAwaiter().GetResult();
                        _connection.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error occurred during RabbitMQ connection disposal");
                }
                finally
                {
                    _disposed = true;
                }
            }
        }
    }
}