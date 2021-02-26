using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Logging;
using OpenA3XX.Core.Repositories;
using OpenA3XX.Core.Sockets.Handlers;

namespace OpenA3XX.Coordinator.SimulatorEventProcessor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(ISimEventingHandler), new FlightSimulatorEventingHandler());
            //services.AddSingleton(typeof(ISimulatorEventsRepository), new SimulatorEventsRepository());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var webSocketOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120)
            };
            app.UseWebSockets(webSocketOptions);
            app.UseLoggingConfiguration(env);


            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var socket = await context.WebSockets.AcceptWebSocketAsync();
                        var simEventingHandler = app.ApplicationServices.GetService<ISimEventingHandler>();
                        //var simulatorEventsRepository = app.ApplicationServices.GetService<ISimulatorEventsRepository>();
                        //if (simEventingHandler != null) await simEventingHandler.Handle(simulatorEventsRepository, socket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });
        }
    }
}