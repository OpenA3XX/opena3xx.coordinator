using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OpenA3XX.Peripheral.WebApi.Hubs
{
    public class ConsumeRabbitMqHostedService : BackgroundService  
{  
    private readonly ILogger _logger;
    private readonly IHubContext<ChatHub> _hubContext;
    private IConnection _connection;  
    private IModel _channel;  
  
    public ConsumeRabbitMqHostedService(ILogger<ConsumeRabbitMqHostedService> logger, IHubContext<ChatHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
        InitRabbitMq();
    }  
  
    private void InitRabbitMq()  
    {  
        var factory = new ConnectionFactory { HostName = "192.168.50.22", UserName = "opena3xx", Password = "opena3xx", VirtualHost = "/"};  
  
        // create connection  
        _connection = factory.CreateConnection();  
  
        // create channel  
        _channel = _connection.CreateModel();  
  
        _channel.QueueDeclare("hardware_events", false, false, false, null);
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
            var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());  
  
            // handle the received message  
            HandleMessage(content);  
            _channel.BasicAck(ea.DeliveryTag, false);  
        };  
  
        consumer.Shutdown += OnConsumerShutdown;  
        consumer.Registered += OnConsumerRegistered;  
        consumer.Unregistered += OnConsumerUnregistered;  
        consumer.ConsumerCancelled += OnConsumerConsumerCancelled;  
  
        _channel.BasicConsume("hardware_events", false, consumer);  
        return Task.CompletedTask;  
    }  
  
    private void HandleMessage(string content)  
    {  
        // we just print this message   
        _hubContext.Clients.All.SendAsync("messageReceivedFromRabbitMq", content);
    }  
      
    private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)  {  }  
    private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) {  }  
    private void OnConsumerRegistered(object sender, ConsumerEventArgs e) {  }  
    private void OnConsumerShutdown(object sender, ShutdownEventArgs e) {  }  
    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)  {  }  
  
    public override void Dispose()  
    {  
        _channel.Close();  
        _connection.Close();  
        base.Dispose();  
    }  
}  
}