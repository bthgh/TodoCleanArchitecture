using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json; 

namespace TodoCleanArchitecture.Infrastructure.Logging;
public class LoggingConfiguration
{
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (context, configuration) =>
    { 
        var env = context.HostingEnvironment;
        
        configuration.Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", env.ApplicationName)
            .Enrich.WithProperty("Environment", env.EnvironmentName); 

        configuration.WriteTo.File(new JsonFormatter(), " /logs/TodoCleanArchitectureLog-.json",rollingInterval: RollingInterval.Day).MinimumLevel.Warning();
        
    };
}
