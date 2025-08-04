using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Repositories;
using RabbitMQ.Client;

namespace OpenA3XX.Core.Services.System
{
    /// <summary>
    /// Service implementation for checking the status of system dependencies
    /// </summary>
    public class DependencyStatusService : IDependencyStatusService
    {
        private readonly ILogger<DependencyStatusService> _logger;
        private readonly RabbitMQOptions _rabbitMqOptions;
        private readonly FlightSimulatorOptions _flightSimulatorOptions;
        private readonly SeqOptions _seqOptions;
        private readonly IHttpClientFactory _httpClientFactory;

        public DependencyStatusService(
            ILogger<DependencyStatusService> logger,
            IOptions<RabbitMQOptions> rabbitMqOptions,
            IOptions<FlightSimulatorOptions> flightSimulatorOptions,
            IOptions<SeqOptions> seqOptions,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _flightSimulatorOptions = flightSimulatorOptions.Value;
            _seqOptions = seqOptions.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DependencyStatusDto> GetDependencyStatusAsync()
        {
            _logger.LogInformation("Checking status of all dependencies");

            var dependencyStatus = new DependencyStatusDto();

            // Check all dependencies in parallel
            var msfsTask = CheckMsfsStatusAsync();
            var rabbitMqTask = CheckRabbitMqStatusAsync();
            var seqTask = CheckSeqStatusAsync();

            await Task.WhenAll(msfsTask, rabbitMqTask, seqTask);

            // Collect results
            dependencyStatus.Dependencies["MSFS"] = await msfsTask;
            dependencyStatus.Dependencies["RabbitMQ"] = await rabbitMqTask;
            dependencyStatus.Dependencies["SEQ"] = await seqTask;

            // Determine overall health - all dependencies must be running
            dependencyStatus.IsHealthy = dependencyStatus.Dependencies["MSFS"].IsRunning &&
                                       dependencyStatus.Dependencies["RabbitMQ"].IsRunning &&
                                       dependencyStatus.Dependencies["SEQ"].IsRunning;

            _logger.LogInformation("Dependency status check completed. Overall health: {IsHealthy}", dependencyStatus.IsHealthy);

            return dependencyStatus;
        }

        public async Task<DependencyDetailDto> CheckMsfsStatusAsync()
        {
            var detail = new DependencyDetailDto();
            
            try
            {
                _logger.LogDebug("Checking MSFS status on {HostName}:{Port}", 
                    _flightSimulatorOptions.Connection.HostName, 
                    _flightSimulatorOptions.Connection.Port);

                // For MSFS, we'll attempt a quick connection test using a timeout
                // Since we can't directly use FsConnect here (different assembly), we'll use a TCP connection test
                using var tcpClient = new global::System.Net.Sockets.TcpClient();
                var connectTask = tcpClient.ConnectAsync(_flightSimulatorOptions.Connection.HostName, (int)_flightSimulatorOptions.Connection.Port);
                var timeoutTask = Task.Delay(5000); // 5 second timeout

                var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                if (completedTask == connectTask && tcpClient.Connected)
                {
                    detail.IsRunning = true;
                    detail.Status = "ok";
                    detail.Metadata["host"] = _flightSimulatorOptions.Connection.HostName;
                    detail.Metadata["port"] = _flightSimulatorOptions.Connection.Port;
                    detail.Metadata["protocol"] = _flightSimulatorOptions.Connection.Protocol;
                    
                    _logger.LogDebug("MSFS is running and accessible");
                }
                else
                {
                    detail.IsRunning = false;
                    detail.Status = "down";
                    detail.Metadata["error"] = "Unable to connect within timeout period";
                    
                    _logger.LogDebug("MSFS connection failed or timed out");
                }
            }
            catch (Exception ex)
            {
                detail.IsRunning = false;
                detail.Status = "down";
                detail.Metadata["error"] = ex.Message;
                detail.Metadata["exception"] = ex.GetType().Name;
                
                _logger.LogWarning(ex, "Failed to check MSFS status");
            }

            return detail;
        }

        public async Task<DependencyDetailDto> CheckRabbitMqStatusAsync()
        {
            var detail = new DependencyDetailDto();

            try
            {
                _logger.LogDebug("Checking RabbitMQ status on {HostName}:{Port}", 
                    _rabbitMqOptions.HostName, _rabbitMqOptions.Port);

                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqOptions.HostName,
                    Port = _rabbitMqOptions.Port,
                    UserName = _rabbitMqOptions.Username,
                    Password = _rabbitMqOptions.Password,
                    VirtualHost = _rabbitMqOptions.VirtualHost,
                    RequestedConnectionTimeout = TimeSpan.FromSeconds(5), // Short timeout for health check
                    SocketReadTimeout = TimeSpan.FromSeconds(5),
                    SocketWriteTimeout = TimeSpan.FromSeconds(5),
                    ClientProvidedName = "health-check"
                };

                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                detail.IsRunning = true;
                detail.Status = "ok";
                detail.Metadata["host"] = _rabbitMqOptions.HostName;
                detail.Metadata["port"] = _rabbitMqOptions.Port;
                detail.Metadata["virtualHost"] = _rabbitMqOptions.VirtualHost;
                detail.Metadata["username"] = _rabbitMqOptions.Username;

                _logger.LogDebug("RabbitMQ is running and accessible");
            }
            catch (Exception ex)
            {
                detail.IsRunning = false;
                detail.Status = "down";
                detail.Metadata["error"] = ex.Message;
                detail.Metadata["exception"] = ex.GetType().Name;

                _logger.LogWarning(ex, "Failed to check RabbitMQ status");
            }

            return detail;
        }

        public async Task<DependencyDetailDto> CheckSeqStatusAsync()
        {
            var detail = new DependencyDetailDto();

            try
            {
                // Get SEQ configuration from appsettings
                var seqEndpoint = GetSeqEndpointFromConfiguration();
                
                _logger.LogDebug("Checking SEQ status at {Endpoint}", seqEndpoint);

                using var httpClient = _httpClientFactory.CreateClient();
                httpClient.Timeout = TimeSpan.FromSeconds(5); // Short timeout for health check

                var response = await httpClient.GetAsync(seqEndpoint);

                // For SEQ health check, any HTTP response (including 404) means it's running
                // SEQ may return 404 for root path but still be operational
                detail.IsRunning = true;
                detail.Status = "ok";
                detail.Metadata["endpoint"] = seqEndpoint;
                detail.Metadata["statusCode"] = (int)response.StatusCode;
                
                if (!response.IsSuccessStatusCode)
                {
                    detail.Metadata["reasonPhrase"] = response.ReasonPhrase;
                }

                _logger.LogDebug("SEQ is running and responding with status code: {StatusCode}", response.StatusCode);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                detail.IsRunning = false;
                detail.Status = "down";
                detail.Metadata["error"] = "Request timed out";

                _logger.LogDebug("SEQ health check timed out");
            }
            catch (HttpRequestException ex)
            {
                detail.IsRunning = false;
                detail.Status = "down";
                detail.Metadata["error"] = ex.Message;
                detail.Metadata["exception"] = ex.GetType().Name;

                _logger.LogWarning(ex, "Failed to check SEQ status via HTTP");
            }
            catch (Exception ex)
            {
                detail.IsRunning = false;
                detail.Status = "down";
                detail.Metadata["error"] = ex.Message;
                detail.Metadata["exception"] = ex.GetType().Name;

                _logger.LogWarning(ex, "Unexpected error checking SEQ status");
            }

            return detail;
        }

        /// <summary>
        /// Gets the SEQ root URL from appsettings configuration for health checking.
        /// Any HTTP response (including 404) indicates SEQ is operational.
        /// </summary>
        /// <returns>The constructed SEQ root URL for health checks</returns>
        private string GetSeqEndpointFromConfiguration()
        {
            try
            {
                // Construct the SEQ root endpoint URL for health checking
                // SEQ doesn't have a dedicated health endpoint, so we check the root URL
                // Any HTTP response (including 404) indicates SEQ is running and operational
                var endpoint = $"http://{_seqOptions.Host}:{_seqOptions.Port}";
                
                _logger.LogDebug("Built SEQ endpoint from appsettings configuration: {Endpoint} (Host: {Host}, Port: {Port})", 
                    endpoint, _seqOptions.Host, _seqOptions.Port);
                
                return endpoint;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get SEQ configuration from appsettings, falling back to default");
                // Fallback to default SEQ configuration if appsettings read fails
                return "http://localhost:5341";
            }
        }
    }
} 