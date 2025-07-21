using System.Collections.Generic;

namespace OpenA3XX.Core.Configuration
{
    /// <summary>
    /// Configuration options for OpenA3XX application settings
    /// </summary>
    public class OpenA3XXOptions
    {
        public const string SectionName = "OpenA3XX";
        
        /// <summary>
        /// Database configuration options
        /// </summary>
        public DatabaseOptions Database { get; set; } = new();
        
        /// <summary>
        /// API configuration options
        /// </summary>
        public ApiOptions Api { get; set; } = new();
    }

    /// <summary>
    /// Database configuration options
    /// </summary>
    public class DatabaseOptions
    {
        /// <summary>
        /// Custom database path (optional)
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }

    /// <summary>
    /// API configuration options
    /// </summary>
    public class ApiOptions
    {
        /// <summary>
        /// Base URL for the API
        /// </summary>
        public string BaseUrl { get; set; } = "http://localhost:5000";
        
        /// <summary>
        /// Allowed CORS origins
        /// </summary>
        public List<string> AllowedCorsOrigins { get; set; } = new() { "http://localhost" };
    }

    /// <summary>
    /// RabbitMQ configuration options
    /// </summary>
    public class RabbitMQOptions
    {
        public const string SectionName = "RabbitMQ";
        
        /// <summary>
        /// RabbitMQ server hostname
        /// </summary>
        public string HostName { get; set; } = "localhost";
        
        /// <summary>
        /// RabbitMQ server port
        /// </summary>
        public int Port { get; set; } = 5672;
        
        /// <summary>
        /// Username for RabbitMQ connection
        /// </summary>
        public string Username { get; set; } = "opena3xx";
        
        /// <summary>
        /// Password for RabbitMQ connection
        /// </summary>
        public string Password { get; set; } = "opena3xx";
        
        /// <summary>
        /// Virtual host for RabbitMQ connection
        /// </summary>
        public string VirtualHost { get; set; } = "/";
        
        /// <summary>
        /// Connection timeout in seconds
        /// </summary>
        public int ConnectionTimeout { get; set; } = 10;
        
        /// <summary>
        /// Socket read timeout in seconds
        /// </summary>
        public int SocketReadTimeout { get; set; } = 10;
        
        /// <summary>
        /// Socket write timeout in seconds
        /// </summary>
        public int SocketWriteTimeout { get; set; } = 10;
        
        /// <summary>
        /// Client provided name for connection identification
        /// </summary>
        public string ClientProvidedName { get; set; } = "app:opena3xx";
        
        /// <summary>
        /// Exchange configuration
        /// </summary>
        public ExchangeOptions Exchanges { get; set; } = new();
        
        /// <summary>
        /// Queue configuration
        /// </summary>
        public QueueOptions Queues { get; set; } = new();
    }

    /// <summary>
    /// RabbitMQ exchange configuration
    /// </summary>
    public class ExchangeOptions
    {
        /// <summary>
        /// Keep alive exchange settings
        /// </summary>
        public ExchangeSettings KeepAlive { get; set; } = new() { Name = "keepalive", Type = "fanout" };
        
        /// <summary>
        /// Hardware input selectors exchange settings
        /// </summary>
        public ExchangeSettings HardwareInputSelectors { get; set; } = new() { Name = "hardware-input-selectors", Type = "fanout" };
    }

    /// <summary>
    /// Individual exchange settings
    /// </summary>
    public class ExchangeSettings
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Exchange type (fanout, direct, topic, headers)
        /// </summary>
        public string Type { get; set; } = "fanout";
    }

    /// <summary>
    /// RabbitMQ queue configuration
    /// </summary>
    public class QueueOptions
    {
        /// <summary>
        /// Simulator test events queue name
        /// </summary>
        public string SimulatorTestEvents { get; set; } = "simulator_test_events";
        
        /// <summary>
        /// MSFS processor queue name
        /// </summary>
        public string ProcessorMsfs { get; set; } = "processor-msfs";
    }

    /// <summary>
    /// Flight Simulator connection configuration
    /// </summary>
    public class FlightSimulatorOptions
    {
        public const string SectionName = "FlightSimulator";
        
        /// <summary>
        /// Connection settings for Flight Simulator
        /// </summary>
        public ConnectionSettings Connection { get; set; } = new();
    }

    /// <summary>
    /// Flight Simulator connection settings
    /// </summary>
    public class ConnectionSettings
    {
        /// <summary>
        /// Flight Simulator hostname or IP address
        /// </summary>
        public string HostName { get; set; } = "127.0.0.1";
        
        /// <summary>
        /// Flight Simulator port
        /// </summary>
        public uint Port { get; set; } = 500;
        
        /// <summary>
        /// Connection protocol (Ipv4, Ipv6, Pipe)
        /// </summary>
        public string Protocol { get; set; } = "Ipv4";
        
        /// <summary>
        /// Application name for SimConnect
        /// </summary>
        public string ApplicationName { get; set; } = "OpenA3XX-Coordinator";
    }

    /// <summary>
    /// External services configuration
    /// </summary>
    public class ExternalServicesOptions
    {
        public const string SectionName = "ExternalServices";
        
        /// <summary>
        /// Coordinator API settings
        /// </summary>
        public CoordinatorApiSettings CoordinatorApi { get; set; } = new();
    }

    /// <summary>
    /// Coordinator API settings
    /// </summary>
    public class CoordinatorApiSettings
    {
        /// <summary>
        /// Base URL for the coordinator API
        /// </summary>
        public string BaseUrl { get; set; } = "http://localhost:5000";
        
        /// <summary>
        /// API endpoint configurations
        /// </summary>
        public EndpointSettings Endpoints { get; set; } = new();
    }

    /// <summary>
    /// API endpoint settings
    /// </summary>
    public class EndpointSettings
    {
        /// <summary>
        /// Hardware input selectors endpoint path
        /// </summary>
        public string HardwareInputSelectors { get; set; } = "hardware-input-selectors";
    }
} 