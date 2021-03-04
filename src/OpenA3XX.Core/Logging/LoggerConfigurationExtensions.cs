using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core.Enrichers;
using Serilog.Enrichers.AspNetCore.HttpContext;

namespace OpenA3XX.Core.Logging
{
    public static class LoggerConfigurationExtensions
    {
        public static void UseLoggingConfiguration(this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment environment)
        {
            Log.Logger = environment.IsDevelopment()
                ? LoggerConfiguration.GetDevelopmentLoggerConfiguration()
                : LoggerConfiguration.GetProductionLoggerConfiguration();

            applicationBuilder.AddHttpContextToLogContext();
        }

        private static void AddHttpContextToLogContext(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSerilogLogContext(options =>
            {
                options.EnrichersForContextFactory = context =>
                {
                    var logEntries = new Dictionary<string, object>();

                    if (context.Request.Headers.TryGetValue("Referer", out var referer))
                        logEntries.Add("Referer", referer.ToString());

                    if (context.Request.Headers.TryGetValue("User-Agent", out var agent))
                        logEntries.Add("User-Agent", agent.ToString());

                    if (context.Request.Headers.TryGetValue("Authorization", out var auth))
                        logEntries.Add("Authorization", auth.ToString());

                    logEntries.Add("IP Address", context.Connection.RemoteIpAddress.ToString());
                    logEntries.Add("Query", context.Request.Query);
                    logEntries.Add("Host", context.Request.Host.Value);
                    logEntries.Add("Method", context.Request.Method);
                    logEntries.Add("Path", context.Request.Path.Value);
                    logEntries.Add("Scheme", context.Request.Scheme);
                    logEntries.Add("Protocol", context.Request.Protocol);
                    logEntries.Add("Username", context.User.Identity.Name);

                    return new[] {new PropertyEnricher("Request", logEntries)};
                };
            });
        }
    }
}