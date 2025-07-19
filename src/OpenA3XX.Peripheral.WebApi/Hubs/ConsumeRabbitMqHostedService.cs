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
using OpenA3XX.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OpenA3XX.Peripheral.WebApi.Hubs
{
    public class ConsumeRabbitMqHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IHubContext<RealtimeHub> _hubContext;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IChannel _hardwareInputSelectorsEventsChannel;
        private IChannel _keepAliveEventsChannel;
        private Dictionary<string, string> _systemConfiguration;

        private MessagingExchangeConfiguration _hardwareInputSelectorsEventsConfig;
        private MessagingExchangeConfiguration _keepAliveExchangeConfig;

        public ConsumeRabbitMqHostedService(ILogger<ConsumeRabbitMqHostedService> logger,
            IHubContext<RealtimeHub> hubContext,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _hubContext = hubContext;
            _serviceProvider = serviceProvider;
            InitRabbitMq();
        }

        private void InitRabbitMq()
        {
            using var scope = _serviceProvider.CreateScope();

            var systemConfigurationRepository =
                scope.ServiceProvider.GetRequiredService<ISystemConfigurationRepository>();

            var configuration = systemConfigurationRepository.GetAllConfiguration()
                .Where(c => c.Key.StartsWith("opena3xx-amqp-"))
                .ToDictionary(
                    k => k.Key,
                    v => v.Value
                );

            _systemConfiguration = configuration;

            var factory = new ConnectionFactory
            {
                HostName = configuration["opena3xx-amqp-host"],
                UserName = configuration["opena3xx-amqp-username"],
                Password = configuration["opena3xx-amqp-password"],
                VirtualHost = configuration["opena3xx-amqp-vhost"]
            };

            // create connection  
            _connection = factory.CreateConnectionAsync().Result;

            //**************************************************************************************************************************
            // create keep alive channel  
            _keepAliveEventsChannel = _connection.CreateChannelAsync().Result;
            var keepAliveConfiguration = configuration["opena3xx-amqp-keepalive-exchange-bindings-configuration"];
            _keepAliveExchangeConfig = new MessagingExchangeConfiguration(keepAliveConfiguration);
            _keepAliveEventsChannel.ExchangeDeclareAsync(_keepAliveExchangeConfig.ExchangeName, "fanout", false, false,
                null);
            foreach (var (queueName, signalrMethodName) in _keepAliveExchangeConfig.QueueSocketBindingConfiguration)
            {
                _keepAliveEventsChannel.QueueDeclareAsync(queueName, false, false, false, new Dictionary<string, object> {{"x-message-ttl", 10000}});
                _keepAliveEventsChannel.QueueBindAsync(queueName, _keepAliveExchangeConfig.ExchangeName, "*", null);
            }

            _keepAliveEventsChannel.BasicQosAsync(0, 1, false);

            //**************************************************************************************************************************

            // create hardware input selector channel 
            _hardwareInputSelectorsEventsChannel = _connection.CreateChannelAsync().Result;
            var hardwareInputSelectorsEventsConfiguration =
                configuration["opena3xx-amqp-hardware-input-selectors-exchange-bindings-configuration"];
            _hardwareInputSelectorsEventsConfig =
                new MessagingExchangeConfiguration(hardwareInputSelectorsEventsConfiguration);
            _hardwareInputSelectorsEventsChannel.ExchangeDeclareAsync(_hardwareInputSelectorsEventsConfig.ExchangeName,
                "fanout", false, false, null);
            foreach (var (queueName, signalrMethodName) in _hardwareInputSelectorsEventsConfig
                .QueueSocketBindingConfiguration)
            {
                _hardwareInputSelectorsEventsChannel.QueueDeclareAsync(queueName, false, false, false, new Dictionary<string, object> {{"x-message-ttl", 10000}});
                _hardwareInputSelectorsEventsChannel.QueueBindAsync(queueName,
                    _hardwareInputSelectorsEventsConfig.ExchangeName, "*", null);
            }

            _hardwareInputSelectorsEventsChannel.BasicQosAsync(0, 1, false);
            //**************************************************************************************************************************

            _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            //**************************************************************************************************************************

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

            _keepAliveEventsChannel.BasicConsumeAsync("admin.keepalive", false, keepAliveBasicConsumer);

            //**************************************************************************************************************************

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
            //hardwareEventingBasicConsumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _hardwareInputSelectorsEventsChannel.BasicConsumeAsync("admin.hardware-input-selectors", false,
                hardwareEventingBasicConsumer);
            return Task.CompletedTask;
        }

        private void HandleHardwareEventMessage(string content)
        {
            foreach (var c in _hardwareInputSelectorsEventsConfig.QueueSocketBindingConfiguration)
            {
                if (c.Item2 != string.Empty)
                {
                    _hubContext.Clients.All.SendAsync(c.Item2, content);
                }
            }
        }

        private void HandleKeepAliveMessage(string content)
        {
            foreach (var c in _keepAliveExchangeConfig.QueueSocketBindingConfiguration)
            {
                if (c.Item2 != string.Empty)
                {
                    _hubContext.Clients.All.SendAsync(c.Item2, content);
                }
            }
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
            _hardwareInputSelectorsEventsChannel.CloseAsync();
            _keepAliveEventsChannel.CloseAsync();
            _connection.CloseAsync();
            base.Dispose();
        }
    }
}