using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OpenA3XX.Peripheral.WebApi.Hubs
{
    /// <summary>
    /// Extension methods for dictionary operations
    /// </summary>
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Gets a value from dictionary or returns default value if key doesn't exist
        /// </summary>
        public static string GetValueOrDefault(this Dictionary<string, string> dictionary, string key, string defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
    public class ConsumeRabbitMqHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IHubContext<RealtimeHub> _hubContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQOptions _rabbitMQOptions;
        private IConnection _connection;
        private IChannel _hardwareInputSelectorsEventsChannel;
        private IChannel _keepAliveEventsChannel;

        public ConsumeRabbitMqHostedService(ILogger<ConsumeRabbitMqHostedService> logger,
            IHubContext<RealtimeHub> hubContext,
            IServiceProvider serviceProvider,
            IOptions<RabbitMQOptions> rabbitMQOptions)
        {
            _logger = logger;
            _hubContext = hubContext;
            _serviceProvider = serviceProvider;
            _rabbitMQOptions = rabbitMQOptions.Value;
            InitRabbitMqWithTimeout();
        }

        private async Task InitRabbitMq()
        {
            _logger.LogInformation("Starting RabbitMQ initialization using configuration");

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQOptions.HostName,
                Port = _rabbitMQOptions.Port,
                UserName = _rabbitMQOptions.Username,
                Password = _rabbitMQOptions.Password,
                VirtualHost = _rabbitMQOptions.VirtualHost,
                RequestedConnectionTimeout = TimeSpan.FromSeconds(_rabbitMQOptions.ConnectionTimeout),
                SocketReadTimeout = TimeSpan.FromSeconds(_rabbitMQOptions.SocketReadTimeout),
                SocketWriteTimeout = TimeSpan.FromSeconds(_rabbitMQOptions.SocketWriteTimeout),
                ClientProvidedName = _rabbitMQOptions.ClientProvidedName
            };
            
            _logger.LogDebug("RabbitMQ connection parameters configured - Host: {Host}:{Port}, Username: {Username}, VHost: {VHost}", 
                _rabbitMQOptions.HostName, 
                _rabbitMQOptions.Port,
                _rabbitMQOptions.Username, 
                _rabbitMQOptions.VirtualHost);
            
            // create connection  
            try
            {
                _logger.LogDebug("Creating RabbitMQ connection");
                _connection = await factory.CreateConnectionAsync();
                _logger.LogInformation("RabbitMQ connection created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create RabbitMQ connection");
                throw;
            }
            // create keep alive channel  
            try
            {
                _logger.LogDebug("Creating keep alive channel");
                _keepAliveEventsChannel = await _connection.CreateChannelAsync();
                _logger.LogDebug("Keep alive channel created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create keep alive channel");
                throw;
            }
            // Setup keep alive exchange using configuration
            await _keepAliveEventsChannel.ExchangeDeclareAsync(_rabbitMQOptions.Exchanges.KeepAlive.Name, 
                _rabbitMQOptions.Exchanges.KeepAlive.Type, false, false, null);
            
            // Create and bind keep alive queue
            await _keepAliveEventsChannel.QueueDeclareAsync("admin.keepalive", false, false, false, 
                new Dictionary<string, object> {{"x-message-ttl", 10000}});
            await _keepAliveEventsChannel.QueueBindAsync("admin.keepalive", _rabbitMQOptions.Exchanges.KeepAlive.Name, "*", null);

            _logger.LogDebug("Setting up keep alive QoS");
            await _keepAliveEventsChannel.BasicQosAsync(0, 1, false);
            _logger.LogDebug("Keep alive QoS configured successfully");

            //**************************************************************************************************************************

            // create hardware input selector channel 
            _logger.LogDebug("Creating hardware input selectors channel");
            _hardwareInputSelectorsEventsChannel = await _connection.CreateChannelAsync();
            _logger.LogDebug("Hardware input selectors channel created successfully");
            
            // Setup hardware input selectors exchange using configuration
            await _hardwareInputSelectorsEventsChannel.ExchangeDeclareAsync(_rabbitMQOptions.Exchanges.HardwareInputSelectors.Name,
                _rabbitMQOptions.Exchanges.HardwareInputSelectors.Type, false, false, null);
            
            // Create and bind hardware input selectors queue
            await _hardwareInputSelectorsEventsChannel.QueueDeclareAsync("admin.hardware-input-selectors", false, false, false, 
                new Dictionary<string, object> {{"x-message-ttl", 10000}});
            await _hardwareInputSelectorsEventsChannel.QueueBindAsync("admin.hardware-input-selectors",
                _rabbitMQOptions.Exchanges.HardwareInputSelectors.Name, "*", null);

            _logger.LogDebug("Setting up hardware input selectors QoS");
            await _hardwareInputSelectorsEventsChannel.BasicQosAsync(0, 1, false);
            _logger.LogDebug("Hardware input selectors QoS configured successfully");

            _logger.LogDebug("Setting up connection shutdown handler");
            _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;
            _logger.LogInformation("RabbitMQ initialization completed successfully");
        }

        private void InitRabbitMqWithTimeout()
        {
            try
            {
                var initTask = InitRabbitMq();
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(30));
                var completedTask = Task.WhenAny(initTask, timeoutTask).GetAwaiter().GetResult();
                
                if (completedTask == timeoutTask)
                {
                    _logger.LogError("RabbitMQ initialization timed out after 30 seconds");
                    throw new TimeoutException("RabbitMQ initialization timed out");
                }
                
                // Wait for and handle any exceptions from the init task
                initTask.GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ initialization failed");
                throw;
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting RabbitMQ message consumption");
            stoppingToken.ThrowIfCancellationRequested();

            _logger.LogDebug("Setting up keep alive consumer");
            var keepAliveBasicConsumer = new AsyncEventingBasicConsumer(_keepAliveEventsChannel);
            keepAliveBasicConsumer.ReceivedAsync += async (ch, ea) =>
            {
                // received message  
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                // handle the received message  
                HandleKeepAliveMessage(content);
                await _keepAliveEventsChannel.BasicAckAsync(ea.DeliveryTag, false);
            };

            keepAliveBasicConsumer.ShutdownAsync += OnConsumerShutdown;
            keepAliveBasicConsumer.RegisteredAsync += OnConsumerRegistered;
            keepAliveBasicConsumer.UnregisteredAsync += OnConsumerUnregistered;
            //keepAliveBasicConsumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _logger.LogDebug("Starting keep alive consumer");
            _keepAliveEventsChannel.BasicConsumeAsync("admin.keepalive", false, keepAliveBasicConsumer);
            _logger.LogDebug("Keep alive consumer started successfully");

            _logger.LogDebug("Setting up hardware events consumer");
            var hardwareEventingBasicConsumer = new AsyncEventingBasicConsumer(_hardwareInputSelectorsEventsChannel);
            hardwareEventingBasicConsumer.ReceivedAsync += async (ch, ea) =>
            {
                // received message  
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                // handle the received message  
                HandleHardwareEventMessage(content);
                await _hardwareInputSelectorsEventsChannel.BasicAckAsync(ea.DeliveryTag, false);
            };

            hardwareEventingBasicConsumer.ShutdownAsync += OnConsumerShutdown;
            hardwareEventingBasicConsumer.RegisteredAsync += OnConsumerRegistered;
            hardwareEventingBasicConsumer.UnregisteredAsync += OnConsumerUnregistered;
            
            _logger.LogDebug("Starting hardware events consumer");
            _hardwareInputSelectorsEventsChannel.BasicConsumeAsync("admin.hardware-input-selectors", false,
                hardwareEventingBasicConsumer);
            _logger.LogDebug("Hardware events consumer started successfully");
            _logger.LogInformation("ConsumeRabbitMqHostedService ExecuteAsync completed successfully");
            return Task.CompletedTask;
        }

        private void HandleHardwareEventMessage(string content)
        {
            // Send to SignalR clients
            _hubContext.Clients.All.SendAsync("HardwareEvent", content);
            _logger.LogDebug("Hardware event message sent to SignalR clients");
        }

        private void HandleKeepAliveMessage(string content)
        {
            // Send to SignalR clients
            _hubContext.Clients.All.SendAsync("KeepAlive", content);
            _logger.LogDebug("Keep alive message sent to SignalR clients");
        }

        private static Task OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            return Task.CompletedTask;
        }

        private static Task OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            return Task.CompletedTask;
        }

        private static Task OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            return Task.CompletedTask;
        }

        private static Task OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            return Task.CompletedTask;
        }

        private static Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            try
            {
                _hardwareInputSelectorsEventsChannel?.CloseAsync().GetAwaiter().GetResult();
                _keepAliveEventsChannel?.CloseAsync().GetAwaiter().GetResult();
                _connection?.CloseAsync().GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                // Ignore disposal errors
            }
            finally
            {
                base.Dispose();
            }
        }
    }
}