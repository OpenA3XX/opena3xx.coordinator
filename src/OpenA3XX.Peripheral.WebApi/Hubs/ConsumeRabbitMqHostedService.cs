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
        private IModel _hardwareInputSelectorsEventsChannel;
        private IModel _keepAliveEventsChannel;
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
            _connection = factory.CreateConnection();

            //**************************************************************************************************************************
            // create keep alive channel  
            _keepAliveEventsChannel = _connection.CreateModel();
            var keepAliveConfiguration =configuration["opena3xx-amqp-keepalive-exchange-bindings-configuration"];
            _keepAliveExchangeConfig = new MessagingExchangeConfiguration(keepAliveConfiguration);
            _keepAliveEventsChannel.ExchangeDeclare(_keepAliveExchangeConfig.ExchangeName,"fanout", false, false,null);
            foreach (var (queueName, signalrMethodName) in _keepAliveExchangeConfig.QueueSocketBindingConfiguration)
            {
                _keepAliveEventsChannel.QueueDeclare(queueName, false, false, false, null);
                _keepAliveEventsChannel.QueueBind(queueName, _keepAliveExchangeConfig.ExchangeName, "*", null);
            }
            _keepAliveEventsChannel.BasicQos(0, 1, false);
            
            //**************************************************************************************************************************
            
            // create hardware input selector channel 
            _hardwareInputSelectorsEventsChannel = _connection.CreateModel();
            var hardwareInputSelectorsEventsConfiguration =configuration["opena3xx-amqp-hardware-input-selectors-exchange-bindings-configuration"];
            _hardwareInputSelectorsEventsConfig = new MessagingExchangeConfiguration(hardwareInputSelectorsEventsConfiguration);
            _hardwareInputSelectorsEventsChannel.ExchangeDeclare(_hardwareInputSelectorsEventsConfig.ExchangeName,"fanout", false, false,null);
            foreach (var (queueName, signalrMethodName) in _hardwareInputSelectorsEventsConfig.QueueSocketBindingConfiguration)
            {
                _hardwareInputSelectorsEventsChannel.QueueDeclare(queueName, false, false, false, null);
                _hardwareInputSelectorsEventsChannel.QueueBind(queueName, _hardwareInputSelectorsEventsConfig.ExchangeName, "*", null);
            }
            _hardwareInputSelectorsEventsChannel.BasicQos(0, 1, false);
            //**************************************************************************************************************************

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            //**************************************************************************************************************************

            var keepAliveBasicConsumer = new EventingBasicConsumer(_keepAliveEventsChannel);
            keepAliveBasicConsumer.Received += (ch, ea) =>
            {
                // received message  
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                // handle the received message  
                HandleKeepAliveMessage(content);
                _keepAliveEventsChannel.BasicAck(ea.DeliveryTag, false);
            };

            keepAliveBasicConsumer.Shutdown += OnConsumerShutdown;
            keepAliveBasicConsumer.Registered += OnConsumerRegistered;
            keepAliveBasicConsumer.Unregistered += OnConsumerUnregistered;
            keepAliveBasicConsumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _keepAliveEventsChannel.BasicConsume("admin.keepalive", false, keepAliveBasicConsumer);

            //**************************************************************************************************************************
            
            var hardwareEventingBasicConsumer = new EventingBasicConsumer(_hardwareInputSelectorsEventsChannel);
            hardwareEventingBasicConsumer.Received += (ch, ea) =>
            {
                // received message  
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                // handle the received message  
                HandleHardwareEventMessage(content);
                _hardwareInputSelectorsEventsChannel.BasicAck(ea.DeliveryTag, false);
            };
            
            hardwareEventingBasicConsumer.Shutdown += OnConsumerShutdown;
            hardwareEventingBasicConsumer.Registered += OnConsumerRegistered;
            hardwareEventingBasicConsumer.Unregistered += OnConsumerUnregistered;
            hardwareEventingBasicConsumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _hardwareInputSelectorsEventsChannel.BasicConsume("admin.hardware-input-selectors", false, hardwareEventingBasicConsumer);
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

        private static void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private static void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private static void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private static void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private static void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _hardwareInputSelectorsEventsChannel.Close();
            _keepAliveEventsChannel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}



