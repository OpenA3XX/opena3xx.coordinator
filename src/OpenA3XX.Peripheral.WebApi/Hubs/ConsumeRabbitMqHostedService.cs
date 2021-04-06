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
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection;  
    private IModel _hardwareEventsChannel;
    private IModel _keepAliveEventsChannel;
    private Dictionary<string, string> _systemConfiguration;
  
    public ConsumeRabbitMqHostedService(ILogger<ConsumeRabbitMqHostedService> logger, IHubContext<ChatHub> hubContext, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _hubContext = hubContext;
        _serviceProvider = serviceProvider;
        InitRabbitMq();
    }  
  
    private void InitRabbitMq()
    {
        using var scope = _serviceProvider.CreateScope();
        
        var systemConfigurationRepository = scope.ServiceProvider.GetRequiredService<ISystemConfigurationRepository>();
            
        var configuration = systemConfigurationRepository.GetAllConfiguration()
            .Where(c => c.Key.StartsWith("opena3xx-amqp-"))
            .ToDictionary(
                k=>k.Key, 
                v=>v.Value
            );

        _systemConfiguration = configuration;
        
        var factory = new ConnectionFactory { 
            HostName = configuration["opena3xx-amqp-host"], 
            UserName = configuration["opena3xx-amqp-username"], 
            Password = configuration["opena3xx-amqp-password"], 
            VirtualHost = configuration["opena3xx-amqp-vhost"]
        };  
  
        // create connection  
        _connection = factory.CreateConnection();  
  
        // create channel  
        _hardwareEventsChannel = _connection.CreateModel();
        _hardwareEventsChannel.QueueDeclare(configuration["opena3xx-amqp-hardware-input-selector-events-queue-name"], false, false, false, null);
        _hardwareEventsChannel.BasicQos(0, 1, false);
        
        _keepAliveEventsChannel = _connection.CreateModel();
        _keepAliveEventsChannel.QueueDeclare(configuration["opena3xx-amqp-keepalive-queue-name"], false, false, false, null);
        _keepAliveEventsChannel.BasicQos(0, 1, false);
  
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }  
  
    protected override Task ExecuteAsync(CancellationToken stoppingToken)  
    {  
        stoppingToken.ThrowIfCancellationRequested();  
  
        
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
        
        _keepAliveEventsChannel.BasicConsume(_systemConfiguration["opena3xx-amqp-keepalive-queue-name"], false, keepAliveBasicConsumer);  
        
        
        
        var hardwareEventingBasicConsumer = new EventingBasicConsumer(_hardwareEventsChannel);  
        hardwareEventingBasicConsumer.Received += (ch, ea) =>  
        {  
            // received message  
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());  
  
            // handle the received message  
            HandleHardwareEventMessage(content);  
            _hardwareEventsChannel.BasicAck(ea.DeliveryTag, false);  
        };  
  
        hardwareEventingBasicConsumer.Shutdown += OnConsumerShutdown;  
        hardwareEventingBasicConsumer.Registered += OnConsumerRegistered;  
        hardwareEventingBasicConsumer.Unregistered += OnConsumerUnregistered;  
        hardwareEventingBasicConsumer.ConsumerCancelled += OnConsumerConsumerCancelled;  
  
        _hardwareEventsChannel.BasicConsume(_systemConfiguration["opena3xx-amqp-hardware-input-selector-events-queue-name"], false, hardwareEventingBasicConsumer);  
        return Task.CompletedTask;  
    }  
  
    private void HandleHardwareEventMessage(string content)  
    {  
        _hubContext.Clients.All.SendAsync(_systemConfiguration["opena3xx-amqp-signalr-hardware-events-proxy-name"], content);
    }  
    
    private void HandleKeepAliveMessage(string content)  
    {  
        _hubContext.Clients.All.SendAsync(_systemConfiguration["opena3xx-amqp-signalr-hardware-keepalive-proxy-name"], content);
    } 
      
    private static void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)  {  }  
    private static void OnConsumerUnregistered(object sender, ConsumerEventArgs e) {  }  
    private static void OnConsumerRegistered(object sender, ConsumerEventArgs e) {  }  
    private static void OnConsumerShutdown(object sender, ShutdownEventArgs e) {  }  
    private static void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)  {  }  
  
    public override void Dispose()  
    {  
        _hardwareEventsChannel.Close();  
        _keepAliveEventsChannel.Close();  
        _connection.Close();  
        base.Dispose();  
    }  
}  
}