using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.Logging;
using OpenA3XX.Peripheral.WebApi.Extensions;
using OpenA3XX.Peripheral.WebApi.Hubs;
using OpenA3XX.Peripheral.WebApi.Middleware;

namespace OpenA3XX.Peripheral.WebApi
{
    /// <summary>
    /// Startup configuration for the OpenA3XX Peripheral WebApi application.
    /// </summary>
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services for dependency injection.
        /// This method gets called by the runtime to add services to the container.
        /// </summary>
        /// <param name="services">The service collection to configure</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure controllers and JSON serialization
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Add SignalR for real-time communication
            services.AddSignalR();
            
            // Configure all OpenA3XX services using extension methods
            services.AddOpenA3XXServices(Configuration);
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// This method gets called by the runtime to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">The web host environment</param>
        /// <param name="logger">The logger instance</param>
        /// <param name="openA3XXOptions">OpenA3XX configuration options</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IOptions<OpenA3XXOptions> openA3XXOptions)
        {
            logger.LogInformation("Configuring OpenA3XX WebApi pipeline");
            
            // Configure global exception handling (must be first)
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            
            // Configure logging middleware
            app.UseLoggingConfiguration(env);

            // Configure development-specific middleware
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenA3XX.Peripheral.WebApi v1"));
                logger.LogInformation("Development environment detected - Swagger UI enabled");
            }

            // Configure routing and CORS
            app.UseRouting();
            app.UseAuthorization();
            
            // Configure CORS with configuration-based origins
            var corsOrigins = openA3XXOptions.Value.Api.AllowedCorsOrigins.ToArray();
            app.UseCors(builder => builder
                .WithOrigins(corsOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials());
            
            logger.LogInformation("CORS configured with origins: {Origins}", string.Join(", ", corsOrigins));
            
            // Configure endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<RealtimeHub>("/signalr");
            });
            
            logger.LogInformation("OpenA3XX WebApi pipeline configuration completed");
        }
    }
}