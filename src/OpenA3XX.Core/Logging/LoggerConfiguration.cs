using System;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;


namespace OpenA3XX.Core.Logging
{
    static class LoggerConfiguration
    {
        internal static Logger GetProductionLoggerConfiguration()
        {
            return new Serilog.LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithAssemblyName()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .Enrich.WithCorrelationId()
                .Enrich.WithAssemblyInformationalVersion()
                .Enrich.WithEnvironment("ASPNETCORE_ENVIRONMENT")
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .CreateLogger();
        }

        internal static Logger GetDevelopmentLoggerConfiguration()
        {
            return new Serilog.LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithAssemblyName()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .Enrich.WithCorrelationId()
                .Enrich.WithEnvironment("ASPNETCORE_ENVIRONMENT")
                // .Enrich.WithAspnetcoreHttpcontext(serviceProvider)
                .Enrich.WithAssemblyInformationalVersion()
                .WriteTo.Seq("http://localhost:5341")
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    theme: AnsiConsoleTheme.Code)
                .CreateLogger();
        }
    }

}