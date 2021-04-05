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
    private IModel _channel;
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
        _channel = _connection.CreateModel();  
  
        _channel.QueueDeclare(configuration["opena3xx-amqp-hardware-input-selector-events-queue-name"], false, false, false, null);
        _channel.BasicQos(0, 1, false);  
  
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }  
  
    protected override Task ExecuteAsync(CancellationToken stoppingToken)  
    {  
        stoppingToken.ThrowIfCancellationRequested();  
  
        var consumer = new EventingBasicConsumer(_channel);  
        consumer.Received += (ch, ea) =>  
        {  
            // received message  
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());  
  
            // handle the received message  
            HandleMessage(content);  
            _channel.BasicAck(ea.DeliveryTag, false);  
        };  
  
        consumer.Shutdown += OnConsumerShutdown;  
        consumer.Registered += OnConsumerRegistered;  
        consumer.Unregistered += OnConsumerUnregistered;  
        consumer.ConsumerCancelled += OnConsumerConsumerCancelled;  
  
        _channel.BasicConsume(_systemConfiguration["opena3xx-amqp-hardware-input-selector-events-queue-name"], false, consumer);  
        return Task.CompletedTask;  
    }  
  
    private void HandleMessage(string content)  
    {  
        _hubContext.Clients.All.SendAsync(_systemConfiguration["opena3xx-amqp-signalr-hardware-events-proxy-name"], content);
    }  
      
    private static void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)  {  }  
    private static void OnConsumerUnregistered(object sender, ConsumerEventArgs e) {  }  
    private static void OnConsumerRegistered(object sender, ConsumerEventArgs e) {  }  
    private static void OnConsumerShutdown(object sender, ShutdownEventArgs e) {  }  
    private static void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)  {  }  
  
    public override void Dispose()  
    {  
        _channel.Close();  
        _connection.Close();  
        base.Dispose();  
    }  
}  
}