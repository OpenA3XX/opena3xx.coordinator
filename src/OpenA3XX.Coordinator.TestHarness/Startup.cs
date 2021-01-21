using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Coordinator.TestHarness
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IServiceProvider provider;
        
        // access the built service pipeline
        public IServiceProvider Provider => provider;

// access the built configuration
        public IConfiguration Configuration => configuration;
        
        public Startup()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            
            // instantiate
            var services = new ServiceCollection();

// add necessary services
            services.AddSingleton<IConfiguration>(configuration);
            
            
            services.AddDbContext<HardwareDataContext>(options =>
            {
                options.UseSqlite("Data Source=hardware.db");
            });

            services.AddScoped<DbContext, HardwareDataContext>();
            
            
            services.AddSingleton<IHardwareComponentRepository, HardwareComponentRepository>();
            
            

// build the pipeline
            provider = services.BuildServiceProvider();

        }
    }
}