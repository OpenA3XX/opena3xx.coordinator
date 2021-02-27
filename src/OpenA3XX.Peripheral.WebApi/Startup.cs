using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Logging;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;
using OpenA3XX.Core.Services;
using Serilog;

namespace OpenA3XX.Peripheral.WebApi
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OpenA3XX.Peripheral.WebApi", Version = "v1",
                    Description =
                        "OpenA3XX Peripheral API to integrate Hardware Panel Cockpits with OpenA3XX Coordinator",
                    License = new OpenApiLicense
                    {
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html")
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "David Bonnici",
                        Email = "davidbonnici1984@gmail.com",
                        Url = new Uri("https://opena3xx.dev")
                    }
                });
            });
            

            services.AddDbContext<CoreDataContext>(options =>
            {
                options.UseSqlite(CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Core));
            });

            // DI Configuration
            services.AddScoped<DbContext, CoreDataContext>();

            services.AddTransient<ISystemConfigurationRepository, SystemConfigurationRepository>();
            services.AddTransient<IHardwarePanelTokensRepository, HardwarePanelTokensRepository>();
            services.AddTransient<IHardwareInputTypesRepository, HardwareInputTypesRepository>();
            services.AddTransient<IHardwareOutputTypesRepository, HardwareOutputTypesRepository>();
            services.AddTransient<IHardwarePanelRepository, HardwarePanelRepository>();
            
            services.AddTransient<ISimulatorEventRepository, SimulatorEventRepository>();
            

            services.AddTransient<IHardwarePanelService, HardwarePanelService>();
            services.AddTransient<IHardwareInputTypeService, HardwareInputTypeService>();
            services.AddTransient<IHardwareOutputTypeService, HardwareOutputTypeService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(HardwarePanelToken)));
            
            
            
            services.AddEasyCaching(option =>
            {
                option.UseInMemory("m1");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseLoggingConfiguration(env);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenA3XX.Peripheral.WebApi v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}