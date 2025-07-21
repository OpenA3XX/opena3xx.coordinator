using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;
using OpenA3XX.Core.Repositories.Base;
using OpenA3XX.Core.Services;
using OpenA3XX.Peripheral.WebApi.Hubs;
using Microsoft.Extensions.Logging;

namespace OpenA3XX.Peripheral.WebApi.Extensions
{
    /// <summary>
    /// Extension methods for configuring services in the DI container
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Swagger documentation services
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OpenA3XX.Peripheral.WebApi",
                    Version = "v1",
                    Description = "OpenA3XX Peripheral API to integrate Hardware Panel Cockpits with OpenA3XX Coordinator",
                    License = new OpenApiLicense
                    {
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html")
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "David Bonnici",
                        Email = "davidbonnici1984@gmail.com",
                        Url = new Uri("https://docs.opena3xx.dev")
                    }
                });
            });

            return services;
        }

        /// <summary>
        /// Configures database context and related services
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddDbContext<CoreDataContext>((serviceProvider, options) =>
            {
                var openA3XXOptions = serviceProvider.GetService<IOptions<OpenA3XXOptions>>()?.Value;
                var databasePath = openA3XXOptions?.Database?.Path;
                var logger = serviceProvider.GetService<ILogger<CoreDataContext>>();
                
                var connectionString = CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Core, databasePath);
                
                // Log database configuration for debugging
                if (logger != null)
                {
                    logger.LogInformation("Database Configuration - ConfigPath: '{ConfigPath}', ConnectionString: '{ConnectionString}'", 
                        databasePath ?? "NULL", connectionString);
                }
                
                options.UseSqlite(connectionString);
                
                // Enable EF Core logging for SQL queries and database operations
                if (logger != null)
                {
                    options.LogTo(message => logger.LogInformation("EF Core SQL: {SqlQuery}", message),
                        new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information);
                        
                    options.EnableSensitiveDataLogging(false); // Don't log sensitive data in production
                    options.EnableDetailedErrors(true);
                    options.EnableServiceProviderCaching(true);
                }
            });
            
            services.AddScoped<DbContext, CoreDataContext>();
            
            return services;
        }

        /// <summary>
        /// Registers all repository interfaces and implementations
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISystemConfigurationRepository>(provider =>
                new SystemConfigurationRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<SystemConfiguration>>>()));
                    
            services.AddTransient<IHardwarePanelTokensRepository>(provider =>
                new HardwarePanelTokensRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<HardwarePanelToken>>>()));
                    
            services.AddTransient<IHardwareInputTypesRepository>(provider =>
                new HardwareInputTypesRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<HardwareInputType>>>()));
                    
            services.AddTransient<IHardwareInputSelectorRepository>(provider =>
                new HardwareInputSelectorRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<HardwareInputSelector>>>()));
                    
            services.AddTransient<IHardwareOutputSelectorRepository>(provider =>
                new HardwareOutputSelectorRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<HardwareOutputSelector>>>()));
                    
            services.AddTransient<IHardwareOutputTypesRepository>(provider =>
                new HardwareOutputTypesRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<HardwareOutputType>>>()));
                    
            services.AddTransient<IHardwarePanelRepository>(provider =>
                new HardwarePanelRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<HardwarePanel>>>()));
                    
            services.AddTransient<IHardwareBoardRepository>(provider =>
                new HardwareBoardRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<HardwareBoard>>>()));
                    
            services.AddTransient<IAircraftModelRepository>(provider =>
                new AircraftModelRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<AircraftModel>>>()));
                    
            services.AddTransient<ISimulatorEventRepository>(provider =>
                new SimulatorEventRepository(
                    provider.GetRequiredService<DbContext>(),
                    provider.GetRequiredService<ILogger<BaseRepository<SimulatorEvent>>>()));
            
            return services;
        }

        /// <summary>
        /// Registers all domain service interfaces and implementations
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<ISimulatorEventService, SimulatorEventService>();
            services.AddTransient<IHardwarePanelService, HardwarePanelService>();
            services.AddTransient<IHardwareBoardService, HardwareBoardService>();
            services.AddTransient<IHardwareInputTypeService, HardwareInputTypeService>();
            services.AddTransient<IHardwareInputSelectorService, HardwareInputSelectorService>();
            services.AddTransient<IHardwareOutputSelectorService, HardwareOutputSelectorService>();
            services.AddTransient<IHardwareOutputTypeService, HardwareOutputTypeService>();
            services.AddTransient<IFormService, FormsService>();
            services.AddTransient<IFlightIntegrationService, FlightIntegrationService>();
            services.AddTransient<ISimulatorEventingService, SimulatorEventingService>();
            
            return services;
        }

        /// <summary>
        /// Configures infrastructure services like AutoMapper, caching, and hosted services
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // HTTP Context Accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // AutoMapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(OpenA3XX.Core.Profiles.HardwarePanelProfile).Assembly);
            });
            services.AddSingleton(mapperConfig);
            services.AddSingleton<IMapper>(provider => new Mapper(mapperConfig));

            // Hosted services
            services.AddHostedService<ConsumeRabbitMqHostedService>();
            
            // Caching
            services.AddEasyCaching(option =>
            {
                option.UseInMemory("m1");
            });
            
            return services;
        }

        /// <summary>
        /// Configures strongly-typed configuration options
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration instance</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind configuration options
            services.Configure<OpenA3XXOptions>(configuration.GetSection(OpenA3XXOptions.SectionName));
            services.Configure<RabbitMQOptions>(configuration.GetSection(RabbitMQOptions.SectionName));
            services.Configure<FlightSimulatorOptions>(configuration.GetSection(FlightSimulatorOptions.SectionName));
            services.Configure<ExternalServicesOptions>(configuration.GetSection(ExternalServicesOptions.SectionName));
            
            // Register as singletons for easy access
            services.AddSingleton<IValidateOptions<OpenA3XXOptions>, ValidateOptionsAdapter<OpenA3XXOptions>>();
            services.AddSingleton<IValidateOptions<RabbitMQOptions>, ValidateOptionsAdapter<RabbitMQOptions>>();
            services.AddSingleton<IValidateOptions<FlightSimulatorOptions>, ValidateOptionsAdapter<FlightSimulatorOptions>>();
            services.AddSingleton<IValidateOptions<ExternalServicesOptions>, ValidateOptionsAdapter<ExternalServicesOptions>>();
            
            return services;
        }

        /// <summary>
        /// Adds all OpenA3XX services in the correct order
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration instance</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddOpenA3XXServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddConfigurationOptions(configuration)
                .AddSwaggerDocumentation()
                .AddDatabaseServices()
                .AddRepositories()
                .AddDomainServices()
                .AddInfrastructureServices();
        }

        /// <summary>
        /// Generic options validation adapter
        /// </summary>
        private class ValidateOptionsAdapter<T> : IValidateOptions<T> where T : class
        {
            public ValidateOptionsResult Validate(string name, T options)
            {
                return ValidateOptionsResult.Success;
            }
        }
    }
} 